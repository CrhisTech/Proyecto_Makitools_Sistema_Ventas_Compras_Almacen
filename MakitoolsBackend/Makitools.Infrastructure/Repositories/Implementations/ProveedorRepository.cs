using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly MakitoolsDbContext _context;
        public ProveedorRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarProveedorAsync(Proveedor proveedor)
        {
            _context.Proveedors.Update(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarProveedorAsync(Proveedor proveedor)
        {
            await _context.Proveedors.AddAsync(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedoresActivosAsync()
        {
            return await _context.Proveedors
                .Where(p => p.Activo == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Proveedor?> ObtenerProveedorPorIdAsync(int id)
        {
            return await _context.Proveedors
                .FirstOrDefaultAsync(p => p.IdProveedor == id);
        }
    }
}
