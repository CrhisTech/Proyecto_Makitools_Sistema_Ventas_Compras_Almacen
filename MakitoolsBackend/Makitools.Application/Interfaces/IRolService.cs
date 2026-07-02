using Makitools.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<RolResponseDto>> ListarRolesAsync();
        Task<RolResponseDto?> BuscarRolPorId(int id);
    }
}
