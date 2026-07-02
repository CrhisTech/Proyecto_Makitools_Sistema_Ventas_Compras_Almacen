using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Makitools.Application.Services;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }
        // GET. /api/Producto/
        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            var productos = await _productoService.ListarProductosAsync();
            return Ok(productos);
        }

        // GET. /api/Producto/{id}
        [HttpGet("{id}", Name = "ProductoCreado")]
        public async Task<IActionResult> BuscarProductoPorId(int id)
        {
            var producto = await _productoService.BuscarProductoPorIdAsync(id);
            return Ok(producto);
        }

        // POST. /api/Producto/
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromForm] ProductoCreateRequestDto request)
        {
            var productoNuevo = await _productoService.CrearProductoAsync(request);
            return StatusCode(201, productoNuevo);
        }

        // PUT. /api/Producto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromForm] ProductoUpdateRequestDto request)
        {
            try
            {
                var resultado = await _productoService.ActualizarProductoAsync(id, request);
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

        // DELETE. /api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                await _productoService.EliminarProductoAsync(id);
                return Ok(new { message = "Producto eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new {message =  ex.Message});
            }
        }
    }
}
