
using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;

namespace Makitools.Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoResponseDto>> ListarProductosAsync();
        Task<ProductoResponseDto?> BuscarProductoPorIdAsync(int id);
        Task<ProductoResponseDto> CrearProductoAsync(ProductoCreateRequestDto dto);
        Task<ProductoResponseDto> ActualizarProductoAsync(int id, ProductoUpdateRequestDto dto);
        Task<bool> EliminarProductoAsync(int id);
    }
}
