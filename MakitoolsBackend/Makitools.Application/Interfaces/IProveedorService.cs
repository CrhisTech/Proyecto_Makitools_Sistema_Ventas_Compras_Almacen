using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorResponseDto>> ListarProveedoresAsync();
        Task<ProveedorResponseDto?> ObtenerProveedorPorIdAsync(int id);
        Task<ProveedorResponseDto> CrearProveedorAsync(ProveedorCreateRequestDto dto);
        Task<ProveedorResponseDto> ActualizarProveedorAsync(int id, ProveedorUpdateRequestDto dto);
        Task<bool> EliminarProveedorAsync(int id);
    }
}
