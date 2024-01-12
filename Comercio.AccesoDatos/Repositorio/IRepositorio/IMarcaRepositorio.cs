using Comercio.Modelos;

namespace Comercio.AccesoDatos.Repositorio.IRepositorio
{
    public interface IMarcaRepositorio : IRepositorio<MarcaModel>
    {
        void Update(MarcaModel marca);
    }
}
