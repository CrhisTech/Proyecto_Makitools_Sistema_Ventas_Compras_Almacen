using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioResponseDto> LoginAsync(LoginRequestDto request)
        {
            var usuariosActivos = await _usuarioRepository.ObtenerUsuariosActivosAsync();

            var usuario = usuariosActivos.FirstOrDefault(u => u.Correo.ToLower() == request.Correo.ToLower());

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Clave, usuario.Clave))
            {
                throw new UnauthorizedAccessException("Correo electrónico o contraseña incorrectos.");
            }

            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                IdRol = usuario.IdRol,
                Rol = usuario.IdRolNavigation.Nombre,
                FechaRegistro = usuario.FechaRegistro,
                Activo = usuario.Activo ?? false
            };
        }
    }
}
