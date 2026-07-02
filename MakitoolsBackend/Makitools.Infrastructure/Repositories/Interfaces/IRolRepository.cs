using Makitools.Domain.Entities.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> ObtenerRolesAsync();
        Task<Rol?> ObtenerUsuarioPorIdAsync(int id);
    }
}
