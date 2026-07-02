using Makitools.Domain.Entities.Almacen;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> ObtenerMarcasActivasAsync();
        Task<Marca?> ObtenerMarcaPorIdAsync(int id);
        Task GuardarMarcaAsync(Marca marca);
    }
}
