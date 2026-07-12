using Makitools.Domain.Entities.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Infrastructure.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObtenerCategoriasActivasAsync();
        Task<Categoria?> ObtenerCategoriaPorIdAsync(int id);
        Task GuardarCategoriaAsync(Categoria categoria);
        Task ActualizarCategoriaAsync(Categoria categoria);
    }
}
