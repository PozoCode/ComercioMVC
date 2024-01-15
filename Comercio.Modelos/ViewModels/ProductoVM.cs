using Microsoft.AspNetCore.Mvc.Rendering;

namespace Comercio.Modelos.ViewModels
{
    public class ProductoVM
    {

        public ProductoModel Producto { get; set; }

        // Se cargará el listado de Categorias en un dropdowlist
        public IEnumerable<SelectListItem> CategoriaList { get; set; }

        // Se cargará el listado de Marcas en un dropdowlist
        public IEnumerable<SelectListItem> MarcaList { get; set; }
    }
}
