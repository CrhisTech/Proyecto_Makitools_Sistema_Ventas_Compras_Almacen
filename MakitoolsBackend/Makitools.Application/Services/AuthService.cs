using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Makitools.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration, IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<UsuarioAuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var usuariosActivos = await _usuarioRepository.ObtenerUsuariosActivosAsync();

            var usuario = usuariosActivos.FirstOrDefault(u => u.Correo.ToLower() == request.Correo.ToLower());

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Clave, usuario.Clave))
            {
                throw new UnauthorizedAccessException("Correo electrónico o contraseña incorrectos.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.IdRol.ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UsuarioAuthResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                IdRol = usuario.IdRol,
                Rol = usuario.IdRolNavigation.Nombre,
                FechaRegistro = usuario.FechaRegistro,
                Activo = usuario.Activo ?? false,
                Token = tokenString
            };
        }

        public async Task<bool> ReestablecerPasswordAsync(ResetPasswordRequestDto request)
        {
            var usuariosActivos = await _usuarioRepository.ObtenerUsuariosActivosAsync();

            var usuario = usuariosActivos.FirstOrDefault(u => u.ResetToken == request.Token);

            if(usuario == null || usuario.ResetTokenExpires < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("El token es inválido o ha caducado.");
            }

            // guardar la nueva clave del usuario
            usuario.Clave = BCrypt.Net.BCrypt.HashPassword(request.NuevaClave);

            usuario.ResetToken = null;
            usuario.ResetTokenExpires = null;

            await _usuarioRepository.ActualizarUsuarioAsync(usuario);

            return true;
        }

        public async Task<bool> SolicitarRecuperacionPasswordAsync(ForgotPasswordRequestDto request)
        {
            var usuariosActivos = await _usuarioRepository.ObtenerUsuariosActivosAsync();
            var usuario = usuariosActivos.FirstOrDefault(u => u.Correo.ToLower() == request.Correo.ToLower());
            if (usuario == null)
            {
                return true;
            }
            usuario.ResetToken = Guid.NewGuid().ToString("N");

            usuario.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);
            await _usuarioRepository.ActualizarUsuarioAsync(usuario);

            var enlaceRecuperacion = $"http://localhost:5173/reset-password?token={usuario.ResetToken}";
            var asunto = "Reestablecer contraseña - Makitools";
            var cuerpoHTML = $@"
                    <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                        <h2>Recuperación de Acceso</h2>
                        <p>Hola <strong>{usuario.Nombres}</strong>,</p>
                        <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en el sistema Makitools.</p>
                        <p>Por favor, haz clic en el siguiente botón para crear una nueva contraseña. Este enlace es válido por 15 minutos.</p>
                        <br/>
                        <a href='{enlaceRecuperacion}' style='padding: 12px 20px; background-color: #4e73df; color: white; text-decoration: none; border-radius: 5px; font-weight: bold;'>
                            Restablecer Contraseña
                        </a>
                        <br/><br/>
                        <p style='font-size: 12px; color: #777;'>Si no solicitaste este cambio, puedes ignorar este correo de forma segura.</p>
                    </div>";
            await _emailService.EnviarCorreoAsync(usuario.Correo, asunto, cuerpoHTML);
            return true;       
        }
    }
}
