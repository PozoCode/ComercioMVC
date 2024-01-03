using Comercio.Modelos;

namespace Comercio.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<CategoriaModel>
    {
        void Update(CategoriaModel categoria);
    }
}
