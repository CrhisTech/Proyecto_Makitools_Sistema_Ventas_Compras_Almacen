using Makitools.Domain.Entities.Almacen;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly MakitoolsDbContext _context;
        public MarcaRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> ObtenerMarcasActivasAsync()
        {
            return await _context.Marcas
                .Where(c => c.Activo == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Marca?> ObtenerMarcaPorIdAsync(int id)
        {
            return await _context.Marcas
                .FirstOrDefaultAsync(c => c.IdMarca == id);
        }

        public async Task GuardarMarcaAsync(Marca marca)
        {
            await _context.Marcas.AddAsync(marca);
            await _context.SaveChangesAsync();
        }
    }
}
