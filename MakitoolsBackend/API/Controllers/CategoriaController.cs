using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Makitools.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }
        // GET. /api/categoria/
        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            var categorias = await _categoriaService.ListarCategoriasAsync();
            return Ok(categorias);
        }

        // GET. /api/categoria/{id}
        [HttpGet("{id}", Name = "CategoriaCreado")]
        public async Task<IActionResult> BuscarCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorIdAsync(id);
            return Ok(categoria);
        }

        // POST. /api/categoria/
        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaCreateRequestDto request)
        {
            var categoriaNuevo = await _categoriaService.CrearCategoriaAsync(request);
            return StatusCode(201, categoriaNuevo);
        }

        // PUT. /api/categoria/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] CategoriaUpdateRequestDto request)
        {
            try
            {
                var resultado = await _categoriaService.ActualizarCategoriaAsync(id, request);
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

        // DELETE. /api/categoria/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            try
            {
                await _categoriaService.EliminarCategoriaAsync(id);
                return Ok(new { message = "Categoria eliminada correctamente." });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
