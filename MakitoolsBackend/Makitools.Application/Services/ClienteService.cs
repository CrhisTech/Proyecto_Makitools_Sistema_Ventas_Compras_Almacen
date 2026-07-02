using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Maestros;
using Makitools.Infrastructure.Repositories.Implementations;
using Makitools.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Services
{
    public class ClienteService : IClienteService
    {
        private  readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<ClienteResponseDto>> ListarClientesAsync()
        {
            var clientes = await _clienteRepository.ObtenerClientesActivosAsync();

            return clientes.Select(c => new ClienteResponseDto
            {
                IdCliente = c.IdCliente,
                TipoDocumento = c.TipoDocumento,
                NumeroDocumento = c.NumeroDocumento,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                Correo = c.Correo,
                Telefono = c.Telefono,
                FechaRegistro = c.FechaRegistro,
                Activo = c.Activo ?? false 
            });
        }

        public async Task<ClienteResponseDto?> ObtenerClientePorIdAsync(int id)
        {
            var c = await _clienteRepository.ObtenerClientePorIdAsync(id);
            if (c == null) return null;

            return new ClienteResponseDto
            {
                IdCliente = c.IdCliente,
                TipoDocumento = c.TipoDocumento,
                NumeroDocumento = c.NumeroDocumento,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                Correo = c.Correo,
                Telefono = c.Telefono,
                FechaRegistro = c.FechaRegistro,
                Activo = c.Activo ?? false
            };
        }

        public async Task<ClienteResponseDto> CrearClienteAsync(ClienteCreateRequestDto dto)
        {
            var clientesExistentes = await _clienteRepository.ObtenerClientesActivosAsync();

            
            if (clientesExistentes.Any(c => c.NumeroDocumento == dto.NumeroDocumento))
                throw new ArgumentException($"El documento '{dto.NumeroDocumento}' ya está registrado.");


            if (!string.IsNullOrWhiteSpace(dto.Correo) && clientesExistentes.Any(c => c.Correo?.ToLower() == dto.Correo.ToLower()))
                throw new ArgumentException($"El correo '{dto.Correo}' ya está en uso por otro cliente.");

            string claveHash = BCrypt.Net.BCrypt.HashPassword(dto.Clave);

            var nuevoCliente = new Cliente
            {
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Clave = claveHash,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            await _clienteRepository.GuardarClienteAsync(nuevoCliente);

            return await ObtenerClientePorIdAsync(nuevoCliente.IdCliente)
                ?? throw new Exception("Error al recuperar el cliente guardado.");
        }

        public async Task<ClienteResponseDto> ActualizarClienteAsync(int id, ClienteUpdateRequestDto dto)
        {
            var clienteExistente = await _clienteRepository.ObtenerClientePorIdAsync(id);
            if (clienteExistente == null || clienteExistente.Activo == false)
                throw new KeyNotFoundException($"El cliente con ID {id} no existe o está inactivo.");

            var todosLosClientes = await _clienteRepository.ObtenerClientesActivosAsync();

            if (todosLosClientes.Any(c => c.NumeroDocumento == dto.NumeroDocumento && c.IdCliente != id))
                throw new ArgumentException($"El documento '{dto.NumeroDocumento}' ya pertenece a otro cliente.");

            if (!string.IsNullOrWhiteSpace(dto.Correo) && todosLosClientes.Any(c => c.Correo?.ToLower() == dto.Correo.ToLower() && c.IdCliente != id))
                throw new ArgumentException($"El correo '{dto.Correo}' ya pertenece a otro cliente.");

            clienteExistente.TipoDocumento = dto.TipoDocumento;
            clienteExistente.NumeroDocumento = dto.NumeroDocumento;
            clienteExistente.Nombres = dto.Nombres;
            clienteExistente.Apellidos = dto.Apellidos;
            clienteExistente.Correo = dto.Correo;
            clienteExistente.Telefono = dto.Telefono;

            if (!string.IsNullOrWhiteSpace(dto.Clave))
            {
                clienteExistente.Clave = BCrypt.Net.BCrypt.HashPassword(dto.Clave);
            }

            await _clienteRepository.ActualizarClienteAsync(clienteExistente);

            return await ObtenerClientePorIdAsync(id)
                ?? throw new Exception("Error al recuperar el cliente actualizado.");
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            var cliente = await _clienteRepository.ObtenerClientePorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"El cliente con ID {id} no existe.");

            cliente.Activo = false;
            await _clienteRepository.ActualizarClienteAsync(cliente);

            return true;
        }
    }
}
