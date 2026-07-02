using Makitools.Domain.Entities.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerProductosActivosAsync();
        Task<Producto?> ObtenerProductoPorIdAsync(int id);
        Task GuardarProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto);
    }
}
