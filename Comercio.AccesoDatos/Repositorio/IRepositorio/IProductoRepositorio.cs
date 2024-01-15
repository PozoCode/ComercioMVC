using Comercio.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Comercio.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<ProductoModel>
    {
        void Update(ProductoModel producto);

        IEnumerable<SelectListItem> GetDropdownList(string obj);
    }
}
