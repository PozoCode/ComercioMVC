using Comercio.Modelos;

namespace Comercio.AccesoDatos.Repositorio.IRepositorio
{
    public interface IBodegaRepositorio : IRepositorio<BodegaModel>
    {
        void Update(BodegaModel model);
    }
}
