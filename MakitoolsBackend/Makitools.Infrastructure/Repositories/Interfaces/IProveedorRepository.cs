
using Makitools.Domain.Entities.Maestros;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedoresActivosAsync();
        Task<Proveedor?> ObtenerProveedorPorIdAsync(int id);
        Task GuardarProveedorAsync(Proveedor proveedor);
        Task ActualizarProveedorAsync(Proveedor proveedor);

    }
}
