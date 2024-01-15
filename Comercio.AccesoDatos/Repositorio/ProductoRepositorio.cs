using Comercio.AccesoDatos.Data;
using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Comercio.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<ProductoModel>, IProductoRepositorio
    {
        private ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Método que actualiza un producto
        /// </summary>
        public void Update(ProductoModel producto)
        {
            var productoBD = _db.Productos.FirstOrDefault(x => x.Id == producto.Id);

            if (productoBD != null)
            {
                productoBD.NumeroSerie = producto.NumeroSerie;
                productoBD.Descripcion = producto.Descripcion;
                productoBD.Precio = producto.Precio;
                productoBD.Costo = producto.Costo;
                productoBD.ImageUrl = producto.ImageUrl;
                productoBD.Estado = producto.Estado;
                productoBD.CategoriaId = producto.CategoriaId;
                productoBD.MarcaId = producto.MarcaId;
                productoBD.PadreId = producto.PadreId;

                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Método que carga el listado de Marca o Categoria
        /// </summary>
        public IEnumerable<SelectListItem> GetDropdownList(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(x => x.Estado == true).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }

            if (obj == "Marca")
            {
                return _db.Marcas.Where(x => x.Estado == true).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }

            return null;
        }
    }
}
