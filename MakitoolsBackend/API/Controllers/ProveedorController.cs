using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarProveedores()
        {
            var proveedores = await _proveedorService.ListarProveedoresAsync();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProveedor(int id)
        {
            var proveedor = await _proveedorService.ObtenerProveedorPorIdAsync(id);
            if (proveedor == null) return NotFound(new { message = "Proveedor no encontrado." });

            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorCreateRequestDto request)
        {
            try
            {
                var resultado = await _proveedorService.CrearProveedorAsync(request);
                return StatusCode(201, resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] ProveedorUpdateRequestDto request)
        {
            try
            {
                var resultado = await _proveedorService.ActualizarProveedorAsync(id, request);
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
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            try
            {
                await _proveedorService.EliminarProveedorAsync(id);
                return Ok(new { message = "Proveedor eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
