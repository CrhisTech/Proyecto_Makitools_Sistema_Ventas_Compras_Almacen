using Makitools.Domain.Entities.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerClientesActivosAsync();
        Task<Cliente?> ObtenerClientePorIdAsync(int id);
        Task GuardarClienteAsync(Cliente cliente);
        Task ActualizarClienteAsync(Cliente cliente);
    }
}
