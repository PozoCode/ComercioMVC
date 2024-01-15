using Comercio.AccesoDatos.Data;
using Comercio.AccesoDatos.Repositorio.IRepositorio;

namespace Comercio.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;

        public IBodegaRepositorio Bodega { get; set; }

        public ICategoriaRepositorio Categoria { get; set; }

        public IMarcaRepositorio Marca { get; set; }

        public IProductoRepositorio Producto { get; set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
        }

        /// <summary>
        /// Método que libera la memoria
        /// </summary>
        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// Método que refleja los cambios en BBDD
        /// </summary>
        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
