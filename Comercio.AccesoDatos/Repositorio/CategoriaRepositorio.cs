using Comercio.AccesoDatos.Data;
using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;

namespace Comercio.AccesoDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<CategoriaModel>, ICategoriaRepositorio
    {
        private ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Método que actualiza una Categoria
        /// </summary>
        public void Update(CategoriaModel categoria)
        {
            var categoriaBD = _db.Bodegas.FirstOrDefault(x => x.Id == categoria.Id);

            if (categoriaBD != null)
            {
                categoriaBD.Nombre = categoria.Nombre;
                categoriaBD.Descripcion = categoria.Descripcion;
                categoriaBD.Estado = categoria.Estado;

                _db.SaveChanges();
            }
        }
    }
}
