using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Compras;
using Makitools.Domain.Enums;
using Makitools.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly List<string> _estadosPermitidos = new List<string>
    { "Pendiente / Emitida", "Aceptada / Confirmada", "Rechazada", "Despachada / En tránsito", "Recibida / Entregada", "Facturada", "Cerrada / Pagada" };

        public CompraService(ICompraRepository compraRepository) { _compraRepository = compraRepository; }

        public async Task<IEnumerable<CompraResponseDto>> ListarComprasAsync()
        {
            var compras = await _compraRepository.ObtenerComprasActivasAsync();
            return compras.Select(c => new CompraResponseDto
            {
                IdCompra = c.IdCompra,
                NumeroOrdenCompra = c.NumeroOrdenCompra,
                Proveedor = c.IdProveedorNavigation?.RazonSocial ?? "Desconocido",
                Usuario = c.IdUsuarioNavigation?.Nombres ?? "Desconocido",
                TipoDocumento = c.TipoDocumento,
                Moneda = c.Moneda,
                FechaCompra = c.FechaCompra,
                FechaEntregaEsperada = c.FechaEntregaEsperada,
                MontoTotal = c.MontoTotal,
                Estado = c.Estado ?? "Pendiente / Emitida"
            }).OrderByDescending(c => c.IdCompra);
        }

        public async Task<CompraResponseDto?> ObtenerCompraPorIdAsync(int id)
        {
            var c = await _compraRepository.ObtenerCompraPorIdAsync(id);
            if (c == null) return null;

            return new CompraResponseDto
            {
                IdCompra = c.IdCompra,
                NumeroOrdenCompra = c.NumeroOrdenCompra,
                Proveedor = c.IdProveedorNavigation?.RazonSocial ?? "Desconocido",
                Usuario = c.IdUsuarioNavigation?.Nombres ?? "Desconocido",
                TipoDocumento = c.TipoDocumento,
                Moneda = c.Moneda,
                FechaCompra = c.FechaCompra,
                FechaEntregaEsperada = c.FechaEntregaEsperada,
                MontoTotal = c.MontoTotal,
                Estado = c.Estado ?? "Pendiente / Emitida"
            };
        }

        public async Task<CompraResponseDto> CrearCompraAsync(CompraCreateRequestDto dto)
        {
            if (dto.Detalles == null || !dto.Detalles.Any()) throw new ArgumentException("El pedido no puede estar vacío.");

            string correlativoGenerado = await _compraRepository.GenerarNumeroOrdenCompraAsync();
            decimal montoTotalCalculado = 0;
            var detallesEntidad = new List<DetalleCompra>();

            foreach (var detalle in dto.Detalles)
            {
                decimal subtotalFila = detalle.Cantidad * detalle.CostoUnitario;
                montoTotalCalculado += subtotalFila;
                detallesEntidad.Add(new DetalleCompra
                {
                    IdProducto = detalle.IdProducto,
                    Cantidad = detalle.Cantidad,
                    CostoUnitario = detalle.CostoUnitario,
                    Total = subtotalFila
                });
            }

            var nuevaCompra = new Compra
            {
                IdProveedor = dto.IdProveedor,
                IdUsuario = dto.IdUsuario,
                TipoDocumento = dto.TipoDocumento,
                NumeroOrdenCompra = correlativoGenerado,
                Moneda = dto.Moneda,
                CondicionesPago = dto.CondicionesPago,
                FechaEntregaEsperada = dto.FechaEntregaEsperada,
                LugarEntrega = dto.LugarEntrega,
                Observaciones = dto.Observaciones,
                FechaCompra = DateTime.Now,
                MontoTotal = montoTotalCalculado,
                Estado = "Pendiente / Emitida",
                DetalleCompras = detallesEntidad
            };

            await _compraRepository.GuardarCompraAsync(nuevaCompra);

            return await ObtenerCompraPorIdAsync(nuevaCompra.IdCompra)
                ?? throw new Exception("Error al recuperar la orden de compra generada.");
        }

        public async Task<CompraResponseDto> ActualizarEstadoCompraAsync(int id, CompraEstadoUpdateRequestDto dto)
        {
            var compraExistente = await _compraRepository.ObtenerCompraPorIdAsync(id);
            if (compraExistente == null) throw new KeyNotFoundException($"La orden {id} no existe.");
            if (!_estadosPermitidos.Contains(dto.Estado)) throw new ArgumentException("Estado no válido.");

            compraExistente.Estado = dto.Estado;
            await _compraRepository.ActualizarCompraAsync(compraExistente);
            return await ObtenerCompraPorIdAsync(id) ?? throw new Exception("Error al recuperar la orden.");
        }
    }
}