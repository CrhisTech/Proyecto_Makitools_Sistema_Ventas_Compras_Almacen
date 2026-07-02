using Makitools.Domain.Entities.Compras;
using Makitools.Domain.Entities.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface ICompraRepository
    {
        Task<IEnumerable<Compra>> ObtenerComprasActivasAsync();
        Task<Compra?> ObtenerCompraPorIdAsync(int id);
        Task GuardarCompraAsync(Compra compra);
        Task ActualizarCompraAsync(Compra compra);
        Task<string> GenerarNumeroOrdenCompraAsync();
    }
}
