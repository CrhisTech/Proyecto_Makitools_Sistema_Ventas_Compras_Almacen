using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _clienteService.ListarClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCliente(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(id);
            if (cliente == null) return NotFound(new { message = "Cliente no encontrado." });

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateRequestDto request)
        {
            try
            {
                var resultado = await _clienteService.CrearClienteAsync(request);
                return StatusCode(201, resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteUpdateRequestDto request)
        {
            try
            {
                var resultado = await _clienteService.ActualizarClienteAsync(id, request);
                return Ok(resultado);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            try
            {
                await _clienteService.EliminarClienteAsync(id);
                return Ok(new { message = "Cliente eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
