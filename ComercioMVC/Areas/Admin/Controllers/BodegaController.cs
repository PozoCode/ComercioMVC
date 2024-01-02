using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;
using Comercio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ComercioMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {
        // Instanciamos la unidad de trabajo para tener acceso a todos nuestros métodos
        private readonly IUnidadTrabajo _unidadTrabajo;

        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var bodegas = await _unidadTrabajo.Bodega.GetAll();
            return View(bodegas);
        }
        #endregion Index

        #region Upsert
        /// <summary>
        /// Método para visualizar el formulario de inserción o actualización de una bogega
        /// </summary>
        /// <returns>Formulario para la actualización o inserción de una bodega</returns>
        public async Task<IActionResult> Upsert(int? id)
        {
            BodegaModel bodega = new BodegaModel();

            if (id == null)
            {
                bodega.Estado = true;
                return View(bodega);
            }

            bodega = await _unidadTrabajo.Bodega.Get(id.GetValueOrDefault());

            if (bodega == null)
            {
                return NotFound();
            }

            return View(bodega);
        }
        #endregion Upsert

        #region PostUpsert
        /// <summary>
        /// Método que realiza la inserción o actualización de la bodega
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BodegaModel bodega)
        {
            if(ModelState.IsValid)
            {
                if (bodega.Id != 0)
                {
                    _unidadTrabajo.Bodega.Update(bodega);
                    TempData[DS.BodegaOk] = "Actualizado correctamente";
                }
                else
                {
                    await _unidadTrabajo.Bodega.Add(bodega);
                    TempData[DS.BodegaOk] = "Agregado correctamente";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(bodega);

        }
        #endregion

        #region Delete
        /// <summary>
        /// Método que permite eliminar una bodega
        /// </summary>
        /// <returns>Json que se usará para las alertas</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var bodega = await _unidadTrabajo.Bodega.Get(id);

            if(bodega == null)
            {
                TempData[DS.BodegaDelete] = "Error al eliminar bodega";
                return Json(new { success = false, message = "Error al borrar registro" });
            }
            _unidadTrabajo.Bodega.Remove(bodega);
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

            var bodegas = await _unidadTrabajo.Bodega.GetAll();

            if (id == 0)
            {
                existe = bodegas.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                existe = bodegas.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != id);
            }

            return Json(new { disponible = !existe });
        }
        

        #endregion
    }
}
