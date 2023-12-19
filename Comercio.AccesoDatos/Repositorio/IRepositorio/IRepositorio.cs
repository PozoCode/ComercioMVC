using System.Linq.Expressions;

namespace Comercio.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {

        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task<T> GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task Add(T entidad);

        void Remove(T entidad);

        void RemoveRange(IEnumerable<T> entidad);
    }
}
