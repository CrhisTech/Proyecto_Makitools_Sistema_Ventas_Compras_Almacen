
using Makitools.Domain.Entities.Maestros;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerUsuariosActivosAsync();
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);
        Task GuardarUsuarioAsync(Usuario usuario);
        Task ActualizarUsuarioAsync(Usuario usuario);
    }
}
