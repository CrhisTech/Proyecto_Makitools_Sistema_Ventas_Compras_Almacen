using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Infrastructure.Repositories.Implementations;
using Makitools.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<RolResponseDto>> ListarRolesAsync()
        {
            var roles = await _rolRepository.ObtenerRolesAsync();
            var rolesDto = roles.Select(r => new RolResponseDto
            {
                IdRol = r.IdRol,
                Nombre = r.Nombre,
                Activo = r.Activo
            });
            return rolesDto;
        }

        public async Task<RolResponseDto?> BuscarRolPorId(int id)
        {
            var rol = await _rolRepository.ObtenerUsuarioPorIdAsync(id);
            if(rol == null)
            {
                throw new KeyNotFoundException($"El rol con id: {id} no fue encontrado.");
            }
            return new RolResponseDto
            {
                IdRol = rol.IdRol,
                Nombre = rol.Nombre,
                Activo = rol.Activo
            };
        }
    }
}
