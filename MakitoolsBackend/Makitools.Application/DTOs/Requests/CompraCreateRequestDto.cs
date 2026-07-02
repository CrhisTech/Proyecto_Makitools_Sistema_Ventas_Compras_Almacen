using Makitools.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class CompraCreateRequestDto
    {
        [Required] public int IdProveedor { get; set; }
        [Required] public int IdUsuario { get; set; }
        [Required] public string TipoDocumento { get; set; } = null!;
        [Required] public string Moneda { get; set; } = null!;
        [Required] public string CondicionesPago { get; set; } = null!;
        [Required] public DateTime FechaEntregaEsperada { get; set; }
        [Required] public string LugarEntrega { get; set; } = null!;
        public string? Observaciones { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "El pedido debe contener al menos un producto.")]
        public List<DetalleCompraRequestDto> Detalles { get; set; } = new List<DetalleCompraRequestDto>();
    }
}

