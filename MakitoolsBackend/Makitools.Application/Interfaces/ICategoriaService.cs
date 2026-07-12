using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponseDto>> ListarCategoriasAsync();
        Task<CategoriaResponseDto?> BuscarCategoriaPorIdAsync(int id);

        Task<CategoriaResponseDto> CrearCategoriaAsync(CategoriaCreateRequestDto dto);
        Task<CategoriaResponseDto> ActualizarCategoriaAsync(int id, CategoriaUpdateRequestDto dto);
        Task<bool> EliminarCategoriaAsync(int id);
    }
}
