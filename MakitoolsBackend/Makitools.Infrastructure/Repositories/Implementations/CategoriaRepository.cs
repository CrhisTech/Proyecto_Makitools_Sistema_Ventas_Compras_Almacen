using Makitools.Domain.Entities.Almacen;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly MakitoolsDbContext _context;
        public CategoriaRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategoriasActivasAsync()
        {
            return await _context.Categorias
                .Where(c => c.Activo == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categoria?> ObtenerCategoriaPorIdAsync(int id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task GuardarCategoriaAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
