using Makitools.Domain.Entities.Compras;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class CompraRepository : ICompraRepository
    {
        private readonly MakitoolsDbContext _context;

        public CompraRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasActivasAsync()
        {
            return await _context.Compras
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .ToListAsync();
        }

        public async Task<Compra?> ObtenerCompraPorIdAsync(int id)
        {
            
            return await _context.Compras
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.DetalleCompras)
                    .ThenInclude(d => d.IdProductoNavigation) 
                .FirstOrDefaultAsync(c => c.IdCompra == id);
        }

        public async Task GuardarCompraAsync(Compra compra)
        {
            await _context.Compras.AddAsync(compra);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCompraAsync(Compra compra)
        {
            _context.Compras.Update(compra);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GenerarNumeroOrdenCompraAsync()
        {
            var resultado = await _context.Database
                .SqlQueryRaw<string>("EXEC [Compras].[sp_GenerarNumeroOrdenCompra]")
                .ToListAsync();

            return resultado.FirstOrDefault() ?? "OC-000001";
        }
    }
}
