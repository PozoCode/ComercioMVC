using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;
using Comercio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ComercioMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        // Instanciamos la unidad de trabajo para tener acceso a todos nuestros métodos
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var categorias = await _unidadTrabajo.Categoria.GetAll();
            return View(categorias);
        }
        #endregion Index

        #region Upsert
        /// <summary>
        /// Método para visualizar el formulario de inserción o actualización de una bogega
        /// </summary>
        /// <returns>Formulario para la actualización o inserción de una categoria</returns>
        public async Task<IActionResult> Upsert(int? id)
        {
            CategoriaModel categoria = new CategoriaModel();

            if (id == null)
            {
                categoria.Estado = true;
                return View(categoria);
            }

            categoria = await _unidadTrabajo.Categoria.Get(id.GetValueOrDefault());

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }
        #endregion Upsert

        #region PostUpsert
        /// <summary>
        /// Método que realiza la inserción o actualización de la bodega
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CategoriaModel categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id != 0)
                {
                    _unidadTrabajo.Categoria.Update(categoria);
                    TempData[DS.BodegaOk] = "Actualizado correctamente";
                }
                else
                {
                    await _unidadTrabajo.Categoria.Add(categoria);
                    TempData[DS.BodegaOk] = "Agregado correctamente";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);

        }
        #endregion

        #region Delete
        /// <summary>
        /// Método que permite eliminar una categoria
        /// </summary>
        /// <returns>Json que se usará para las alertas</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _unidadTrabajo.Categoria.Get(id);

            if (categoria == null)
            {
                TempData[DS.BodegaDelete] = "Error al eliminar bodega";
                return Json(new { success = false, message = "Error al borrar registro" });
            }
            _unidadTrabajo.Categoria.Remove(categoria);
            await _unidadTrabajo.Guardar();

            TempData[DS.BodegaOk] = "Se ha eliminado un registro";
            return Json(new { success = true, message = "Bodega borrada exitosamente" });

        }

        #endregion

        #region ApiObtenerTodos
        [HttpGet]
        /// <summary>
        /// Método que obtiene todas las bodegas
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ObtenerTodos()
        {
            var bodegas = await _unidadTrabajo.Bodega.GetAll();
            return Json(new { bodegas = bodegas });
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

            var categorias = await _unidadTrabajo.Categoria.GetAll();

            if (id == 0)
            {
                existe = categorias.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                existe = categorias.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != id);
            }

            return Json(new { disponible = !existe });
        }


        #endregion
    }
}
