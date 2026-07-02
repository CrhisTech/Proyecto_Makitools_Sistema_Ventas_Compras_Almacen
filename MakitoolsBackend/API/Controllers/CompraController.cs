using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;
        public CompraController(ICompraService compraService) { _compraService = compraService; }

        [HttpGet]
        public async Task<IActionResult> ListarCompras()
        {
            var compras = await _compraService.ListarComprasAsync();
            return Ok(compras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCompra(int id)
        {
            var compra = await _compraService.ObtenerCompraPorIdAsync(id);
            if (compra == null) return NotFound(new { message = "Orden de compra no encontrada." });
            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPedido([FromBody] CompraCreateRequestDto request)
        {
            try { return StatusCode(201, await _compraService.CrearCompraAsync(request)); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CompraEstadoUpdateRequestDto request)
        {
            try { return Ok(await _compraService.ActualizarEstadoCompraAsync(id, request)); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}
