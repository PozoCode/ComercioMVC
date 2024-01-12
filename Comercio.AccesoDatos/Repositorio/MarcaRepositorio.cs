using Comercio.AccesoDatos.Data;
using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;

namespace Comercio.AccesoDatos.Repositorio
{
    public class MarcaRepositorio : Repositorio<MarcaModel>, IMarcaRepositorio
    {
        private ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Método que actualiza una Marca
        /// </summary>
        public void Update(MarcaModel marca)
        {
            var marcaBD = _db.Marcas.FirstOrDefault(x => x.Id == marca.Id);

            if (marcaBD != null)
            {
                marcaBD.Nombre = marca.Nombre;
                marcaBD.Descripcion = marca.Descripcion;
                marcaBD.Estado = marca.Estado;

                _db.SaveChanges();
            }
        }
    }
}
