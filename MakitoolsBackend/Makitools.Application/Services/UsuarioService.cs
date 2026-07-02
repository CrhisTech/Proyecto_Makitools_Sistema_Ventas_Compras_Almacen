using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Maestros;
using Makitools.Domain.Enums;
using Makitools.Infrastructure.Repositories.Interfaces;

namespace Makitools.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ListarUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.ObtenerUsuariosActivosAsync();
            var usuariosDto = usuarios.Select(u => new UsuarioResponseDto
            {
                IdUsuario = u.IdUsuario,
                Nombres = u.Nombres,
                Apellidos = u.Apellidos,
                Correo = u.Correo,
                Rol = u.IdRolNavigation.Nombre,
                FechaRegistro = u.FechaRegistro,
                Activo = u.Activo ?? false
            }); 
            return usuariosDto;
        }

        public async Task<UsuarioResponseDto?> BuscarUsuarioPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario con id: {id} no fue encontrado");
            }
            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                Rol = usuario.IdRolNavigation.Nombre,
                FechaRegistro = usuario.FechaRegistro,
                Activo = usuario.Activo ?? false
            };
        }

        public async Task<UsuarioResponseDto> CrearUsuarioAsync(UsuarioCreateRequestDto dto)
        {
            if(!Enum.IsDefined(typeof(RolEnum), dto.IdRol))
            {
                throw new ArgumentException($"El rol seleccionado: {dto.IdRol} no es valido");
            }

            string claveHash = BCrypt.Net.BCrypt.HashPassword(dto.Clave);

            var nuevoUsuario = new Usuario
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                Clave = claveHash,
                IdRol = (int)(RolEnum)dto.IdRol,
                Activo = true,
                Reestablecer = true,
                FechaRegistro = DateTime.Now
            };

            await _usuarioRepository.GuardarUsuarioAsync(nuevoUsuario);

            var usuarioGuardado = await _usuarioRepository.ObtenerUsuarioPorIdAsync(nuevoUsuario.IdUsuario);
            
            return new UsuarioResponseDto
            {
                IdUsuario = usuarioGuardado!.IdUsuario,
                Nombres = usuarioGuardado.Nombres,
                Apellidos = usuarioGuardado.Apellidos,
                Correo = usuarioGuardado.Correo,
                Rol = usuarioGuardado.IdRolNavigation.Nombre,
                FechaRegistro = usuarioGuardado.FechaRegistro,
            };
        }

        public async Task<UsuarioResponseDto> ActualizarUsuarioAsync(int id, UsuarioUpdateRequestDto dto)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerUsuarioPorIdAsync(id);
            if (usuarioExistente == null || usuarioExistente.Activo == false)
            {
                throw new KeyNotFoundException($"El usuario con id: {id} no fue encontrado o está inactivo.");
            }

            if (!Enum.IsDefined(typeof(RolEnum), dto.IdRol))
            {
                throw new ArgumentException($"El rol seleccionado: {dto.IdRol} no es válido.");
            }

            var todosLosUsuarios = await _usuarioRepository.ObtenerUsuariosActivosAsync();
            if (todosLosUsuarios.Any(u => u.Correo.ToLower() == dto.Correo.ToLower() && u.IdUsuario != id))
            {
                throw new ArgumentException($"El correo '{dto.Correo}' ya pertenece a otro usuario registrado.");
            }

            usuarioExistente.Nombres = dto.Nombres;
            usuarioExistente.Apellidos = dto.Apellidos;
            usuarioExistente.Correo = dto.Correo;
            usuarioExistente.IdRol = dto.IdRol;

            if (!string.IsNullOrWhiteSpace(dto.Clave))
            {
                usuarioExistente.Clave = BCrypt.Net.BCrypt.HashPassword(dto.Clave);
                usuarioExistente.Reestablecer = true;
            }

            await _usuarioRepository.ActualizarUsuarioAsync(usuarioExistente);

            return await BuscarUsuarioPorIdAsync(id)
                ?? throw new Exception("Error al recuperar el usuario actualizado.");
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario con id: {id} no fue encontrado.");
            }

            usuario.Activo = false;
            await _usuarioRepository.ActualizarUsuarioAsync(usuario);

            return true;
        }
    }
}
