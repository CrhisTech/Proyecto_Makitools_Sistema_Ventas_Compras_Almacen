using Makitools.Application.DTOs.Requests;
using Makitools.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var usuario = await _authService.LoginAsync(request);
                return Ok(usuario);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(new {message =  ex.Message});
            }
        }
    }
}
