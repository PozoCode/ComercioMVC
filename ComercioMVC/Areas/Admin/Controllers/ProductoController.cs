using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;
using Comercio.Modelos.ViewModels;
using Comercio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ComercioMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        // Instanciamos la unidad de trabajo para tener acceso a todos nuestros métodos
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProductoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var productos = await _unidadTrabajo.Producto.GetAll();
            return View(productos);
        }
        #endregion Index

        #region GetUpsert
        /// <summary>
        /// Método para visualizar el formulario de inserción o actualización de un producto
        /// </summary>
        /// <returns>Formulario para la actualización o inserción de una producto</returns>
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new ProductoModel(),
                CategoriaList = _unidadTrabajo.Producto.GetDropdownList("Categoria"),
                MarcaList = _unidadTrabajo.Producto.GetDropdownList("Marca")
            };

            if (id == null)
            {
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = await _unidadTrabajo.Producto.Get(id.GetValueOrDefault());

                if(productoVM.Producto == null)
                {
                    return NotFound();
                }

                return View(productoVM);
            }
        }
        #endregion GetUpsert

        #region PostUpsert
        /// <summary>
        /// Método que realiza la inserción o actualización de una marca
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MarcaModel marca)
        {
            return View();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Método que permite eliminar un producto
        /// </summary>
        /// <returns>Json que se usará para las alertas</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unidadTrabajo.Producto.Get(id);

            if (producto == null)
            {
                TempData[DS.BodegaDelete] = "Error al eliminar producto";
                return Json(new { success = false, message = "Error al borrar registro" });
            }
            _unidadTrabajo.Producto.Remove(producto);
            await _unidadTrabajo.Guardar();

            TempData[DS.BodegaOk] = $"Se ha eliminado el producto {producto.NumeroSerie}";
            return Json(new { success = true, message = "marca borrada exitosamente" });

        }

        #endregion

        #region ApiObtenerTodos
        [HttpGet]
        /// <summary>
        /// Método que obtiene todos los productos
        /// </summary>
        /// <returns>Todos los productos</returns>
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _unidadTrabajo.Producto.GetAll(incluirPropiedades:"Categoria,Marca");
            //var productos = await _unidadTrabajo.Producto.GetAll();
            return Json(new { productos = productos });
        }

        #endregion ApiObtenerTodos

        #region ValidarNombre
        /// <summary>
        /// Método que valida si ya existe un producto con el mismo numero de serie
        /// </summary>
        /// <returns></returns>
        [ActionName("ValidarSerie")]
        public async Task<IActionResult> ValidarSerie(string numeroSerie, int id = 0)
        {
            bool existe = false;

            var productos = await _unidadTrabajo.Producto.GetAll();

            if (id == 0)
            {
                existe = productos.Any(x => x.NumeroSerie.ToLower().Trim() == numeroSerie.ToLower().Trim());
            }
            else
            {
                existe = productos.Any(x => x.NumeroSerie.ToLower().Trim() == numeroSerie.ToLower().Trim() && x.Id != id);
            }

            return Json(new { disponible = !existe });
        }

        #endregion ValidarNombre
    }
}
