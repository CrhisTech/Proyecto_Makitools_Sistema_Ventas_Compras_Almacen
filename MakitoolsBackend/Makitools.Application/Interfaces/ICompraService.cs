using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;


namespace Makitools.Application.Interfaces
{
    public interface ICompraService
    {
        Task<IEnumerable<CompraResponseDto>> ListarComprasAsync();
        Task<CompraResponseDto?> ObtenerCompraPorIdAsync(int id);
        Task<CompraResponseDto> CrearCompraAsync(CompraCreateRequestDto dto);
        Task<CompraResponseDto> ActualizarEstadoCompraAsync(int id, CompraEstadoUpdateRequestDto dto);
    }
}
