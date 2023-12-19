namespace Comercio.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {

        IBodegaRepositorio Bodega { get; }

        Task Guardar();
    }
}
