using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class RolRepository: IRolRepository
    {
        private readonly MakitoolsDbContext _context;
        public RolRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> ObtenerRolesAsync()
        {
            return await _context.Rols
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Rol?> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _context.Rols.FirstOrDefaultAsync(r => r.IdRol == id);
        }
    }
}
