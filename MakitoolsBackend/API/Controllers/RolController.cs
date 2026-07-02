using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Makitools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarRoles()
        {
            var roles = await _rolService.ListarRolesAsync();
            return Ok(roles);
        }
    }
}
