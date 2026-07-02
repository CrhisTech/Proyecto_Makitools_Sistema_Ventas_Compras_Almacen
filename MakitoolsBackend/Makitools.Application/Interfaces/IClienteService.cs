using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;


namespace Makitools.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponseDto>> ListarClientesAsync();
        Task<ClienteResponseDto?> ObtenerClientePorIdAsync(int id);
        Task<ClienteResponseDto> CrearClienteAsync(ClienteCreateRequestDto dto);
        Task<ClienteResponseDto> ActualizarClienteAsync(int id, ClienteUpdateRequestDto dto);
        Task<bool> EliminarClienteAsync(int id);
    }
}
