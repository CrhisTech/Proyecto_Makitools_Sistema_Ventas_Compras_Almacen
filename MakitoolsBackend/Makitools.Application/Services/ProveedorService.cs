using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;
        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }
        public async Task<IEnumerable<ProveedorResponseDto>> ListarProveedoresAsync()
        {
            var proveedores = await _proveedorRepository.ObtenerProveedoresActivosAsync();

            return proveedores.Select(p => new ProveedorResponseDto
            {
                IdProveedor = p.IdProveedor,
                Ruc = p.Ruc,
                RazonSocial = p.RazonSocial,
                Contacto = p.Contacto,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Activo = p.Activo ?? false
            });
        }

        public async Task<ProveedorResponseDto?> ObtenerProveedorPorIdAsync(int id)
        {
            var p = await _proveedorRepository.ObtenerProveedorPorIdAsync(id);
            if (p == null) return null;

            return new ProveedorResponseDto
            {
                IdProveedor = p.IdProveedor,
                Ruc = p.Ruc,
                RazonSocial = p.RazonSocial,
                Contacto = p.Contacto,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Activo = p.Activo ?? false
            };
        }

        public async Task<ProveedorResponseDto> CrearProveedorAsync(ProveedorCreateRequestDto dto)
        {
            var proveedoresExistentes = await _proveedorRepository.ObtenerProveedoresActivosAsync();
            bool rucDuplicado = proveedoresExistentes.Any(p => p.Ruc == dto.Ruc);

            if (rucDuplicado)
                throw new ArgumentException($"El RUC '{dto.Ruc}' ya se encuentra registrado.");

            var nuevoProveedor = new Proveedor
            {
                Ruc = dto.Ruc,
                RazonSocial = dto.RazonSocial,
                Contacto = dto.Contacto,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Activo = true
            };

            await _proveedorRepository.GuardarProveedorAsync(nuevoProveedor);

            return await ObtenerProveedorPorIdAsync(nuevoProveedor.IdProveedor)
                ?? throw new Exception("Error al recuperar el proveedor guardado.");
        }

        public async Task<ProveedorResponseDto> ActualizarProveedorAsync(int id, ProveedorUpdateRequestDto dto)
        {
            var proveedorExistente = await _proveedorRepository.ObtenerProveedorPorIdAsync(id);
            if (proveedorExistente == null || proveedorExistente.Activo == false)
                throw new KeyNotFoundException($"El proveedor con ID {id} no existe o está inactivo.");

            var todosLosProveedores = await _proveedorRepository.ObtenerProveedoresActivosAsync();
            bool rucDuplicado = todosLosProveedores.Any(p => p.Ruc == dto.Ruc && p.IdProveedor != id);

            if (rucDuplicado)
                throw new ArgumentException($"El RUC '{dto.Ruc}' ya está siendo usado por otro proveedor.");

            proveedorExistente.Ruc = dto.Ruc;
            proveedorExistente.RazonSocial = dto.RazonSocial;
            proveedorExistente.Contacto = dto.Contacto;
            proveedorExistente.Telefono = dto.Telefono;
            proveedorExistente.Correo = dto.Correo;

            await _proveedorRepository.ActualizarProveedorAsync(proveedorExistente);

            return await ObtenerProveedorPorIdAsync(id)
                ?? throw new Exception("Error al recuperar el proveedor actualizado.");
        }

        public async Task<bool> EliminarProveedorAsync(int id)
        {
            var proveedor = await _proveedorRepository.ObtenerProveedorPorIdAsync(id);
            if (proveedor == null)
                throw new KeyNotFoundException($"El proveedor con ID {id} no existe.");

            proveedor.Activo = false;
            await _proveedorRepository.ActualizarProveedorAsync(proveedor);

            return true;
        }
    }
}
