using Microsoft.AspNetCore.Mvc;
using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;


namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET. /api/usuario/
        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();
            return Ok(usuarios);
        }

        // GET. /api/usuario/{id}
        [HttpGet("{id}", Name = "UsuarioCreado")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);
            return Ok(usuario);
        }

        // POST. /api/usuario/
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCreateRequestDto request)
        {
            var usuarioNuevo = await _usuarioService.CrearUsuarioAsync(request);
            return StatusCode(201, usuarioNuevo);
        }

        // PUT. /api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioUpdateRequestDto request)
        {
            try
            {
                var usuarioActualizado = await _usuarioService.ActualizarUsuarioAsync(id, request);
                return Ok(usuarioActualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE. /api/usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _usuarioService.EliminarUsuarioAsync(id);
                return Ok(new { message = "Usuario eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
