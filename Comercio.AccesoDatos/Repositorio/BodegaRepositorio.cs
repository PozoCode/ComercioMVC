using Comercio.AccesoDatos.Data;
using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Comercio.Modelos;

namespace Comercio.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<BodegaModel>, IBodegaRepositorio
    {
        private ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Método que actualiza un registro
        /// </summary>
        public void Update(BodegaModel model)
        {
            var bodegaBD = _db.Bodegas.FirstOrDefault(x => x.Id == model.Id);

            if (bodegaBD != null)
            {
                bodegaBD.Nombre = model.Nombre;
                bodegaBD.Descripcion = model.Descripcion;
                bodegaBD.Estado = model.Estado;

                _db.SaveChanges();
            }
        }
    }
}
