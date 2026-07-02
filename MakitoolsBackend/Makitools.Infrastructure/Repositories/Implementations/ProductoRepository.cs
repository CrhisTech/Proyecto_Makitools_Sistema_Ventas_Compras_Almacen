using Makitools.Domain.Entities.Almacen;
using Makitools.Infrastructure.Context;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Implementations
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MakitoolsDbContext _context;
        public ProductoRepository(MakitoolsDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarProductoAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<Producto?> ObtenerProductoPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosActivosAsync()
        {
            return await _context.Productos
                .Include(p =>p.IdMarcaNavigation)
                .Include (p => p.IdCategoriaNavigation)
                .Where( p => p.Activo == true)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
