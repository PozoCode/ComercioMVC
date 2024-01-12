using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;
using Comercio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ComercioMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        // Instanciamos la unidad de trabajo para tener acceso a todos nuestros métodos
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var marcas = await _unidadTrabajo.Marca.GetAll();
            return View(marcas);
        }
        #endregion Index

        #region Upsert
        /// <summary>
        /// Método para visualizar el formulario de inserción o actualización de una marca
        /// </summary>
        /// <returns>Formulario para la actualización o inserción de una marca</returns>
        public async Task<IActionResult> Upsert(int? id)
        {
            MarcaModel marca = new MarcaModel();

            if (id == null)
            {
                marca.Estado = true;
                return View(marca);
            }

            marca = await _unidadTrabajo.Marca.Get(id.GetValueOrDefault());

            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }
        #endregion Upsert

        #region PostUpsert
        /// <summary>
        /// Método que realiza la inserción o actualización de una marca
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MarcaModel marca)
        {
            if (ModelState.IsValid)
            {
                if (marca.Id != 0)
                {
                    _unidadTrabajo.Marca.Update(marca);
                    TempData[DS.BodegaOk] = "Actualizado correctamente";
                }
                else
                {
                    await _unidadTrabajo.Marca.Add(marca);
                    TempData[DS.BodegaOk] = "Agregado correctamente";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(marca);

        }
        #endregion

        #region Delete
        /// <summary>
        /// Método que permite eliminar una marca
        /// </summary>
        /// <returns>Json que se usará para las alertas</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _unidadTrabajo.Marca.Get(id);

            if (marca == null)
            {
                TempData[DS.BodegaDelete] = "Error al eliminar marca";
                return Json(new { success = false, message = "Error al borrar registro" });
            }
            _unidadTrabajo.Marca.Remove(marca);
            await _unidadTrabajo.Guardar();

            TempData[DS.BodegaOk] = "Se ha eliminado un registro";
            return Json(new { success = true, message = "marca borrada exitosamente" });

        }

        #endregion

        #region ApiObtenerTodos
        [HttpGet]
        /// <summary>
        /// Método que obtiene todas las marcas
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ObtenerTodos()
        {
            var marcas = await _unidadTrabajo.Marca.GetAll();
            return Json(new { marcas = marcas });
        }

        #endregion ApiObtenerTodos

        #region ValidarNombre
        /// <summary>
        /// Método que valida si ya existe un registro con el mismo nombre
        /// </summary>
        /// <returns></returns>
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool existe = false;

            var marcas = await _unidadTrabajo.Marca.GetAll();

            if (id == 0)
            {
                existe = marcas.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                existe = marcas.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != id);
            }

            return Json(new { disponible = !existe });
        }


        #endregion
    }
}
