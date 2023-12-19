using Comercio.AccesoDatos.Data;
using Comercio.AccesoDatos.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comercio.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        // Referencia al dbcontext (para la conexión a BBDD)
        private readonly ApplicationDbContext _db;

        // Referencia al DBSet del modelo que llamaremos
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Add(T entidad)
        {
            await dbSet.AddAsync(entidad);      // insert into table
        }

        /// <summary>
        /// Método para obtener un registro mediante un id
        /// </summary>
        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);   // select * from X where id
        }

        /// <summary>
        /// Método que obtiene todos los registros
        /// </summary>
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if(filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach(var propiedades in incluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedades); // Trae productos relacionados, ejemplo, "Marca, modelo"
                }
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Método para obtener el primer valor que cumpla las condiciones que se estipulan
        /// </summary>
        public async Task<T> GetFirst(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach (var propiedades in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedades); // Trae productos relacionados, ejemplo, "Marca, modelo"
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Método para remover un registro
        /// </summary>
        public void Remove(T entidad)
        {
            dbSet.Remove(entidad);
        }

        /// <summary>
        /// Método para remover un grupo de registros
        /// </summary>
        public void RemoveRange(IEnumerable<T> entidad)
        {
           dbSet.RemoveRange(entidad);
        }
    }
}
