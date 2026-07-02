using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MakitoolsDbContext _context;
        public UsuarioRepository(MakitoolsDbContext context)
        { 
           _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosActivosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .Where(u => u.Activo == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task GuardarUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
