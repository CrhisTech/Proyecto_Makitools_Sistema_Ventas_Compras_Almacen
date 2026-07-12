using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Makitools.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        // GET. /api/Marca/
        [HttpGet]
        public async Task<IActionResult> ListarMarcas()
        {
            var Marcas = await _marcaService.ListarMarcasAsync();
            return Ok(Marcas);
        }

        // GET. /api/Marca/{id}
        [HttpGet("{id}", Name = "MarcaCreado")]
        public async Task<IActionResult> BuscarMarcaPorId(int id)
        {
            var Marca = await _marcaService.BuscarMarcaPorIdAsync(id);
            return Ok(Marca);
        }

        // POST. /api/Marca/
        [HttpPost]
        public async Task<IActionResult> CrearMarca([FromBody] MarcaCreateRequestDto request)
        {
            var MarcaNuevo = await _marcaService.CrearMarcaAsync(request);
            return StatusCode(201, MarcaNuevo);
        }

        // PUT. /api/marca/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMarca(int id, [FromBody] MarcaUpdateRequestDto request)
        {
            try
            {
                var resultado = await _marcaService.ActualizarMarcaAsync(id, request);
                return Ok(resultado);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE. /api/marca/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMarca(int id)
        {
            try
            {
                await _marcaService.EliminarMarcaAsync(id);
                return Ok(new { message = "Marca eliminada correctamente." });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
