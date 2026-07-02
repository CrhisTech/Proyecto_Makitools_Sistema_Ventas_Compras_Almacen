using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MakitoolsDbContext _context;
        public ClienteRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(int id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesActivosAsync()
        {
            return await _context.Clientes
                .Where(c => c.Activo == true)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
