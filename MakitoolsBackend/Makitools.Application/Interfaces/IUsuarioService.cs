using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDto>> ListarUsuariosAsync();
        Task<UsuarioResponseDto?> BuscarUsuarioPorIdAsync(int id);

        Task<UsuarioResponseDto> CrearUsuarioAsync(UsuarioCreateRequestDto dto);

        Task<UsuarioResponseDto> ActualizarUsuarioAsync(int id, UsuarioUpdateRequestDto dto);

        Task<bool> EliminarUsuarioAsync(int id);
    }
}
