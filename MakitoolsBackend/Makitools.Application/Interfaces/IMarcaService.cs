using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;


namespace Makitools.Application.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaResponseDto>> ListarMarcasAsync();
        Task<MarcaResponseDto?> BuscarMarcaPorIdAsync(int id);

        Task<MarcaResponseDto> CrearMarcaAsync(MarcaCreateRequestDto dto);
        Task<MarcaResponseDto> ActualizarMarcaAsync(int id, MarcaUpdateRequestDto dto);
        Task<bool> EliminarMarcaAsync(int id);
    }
}
