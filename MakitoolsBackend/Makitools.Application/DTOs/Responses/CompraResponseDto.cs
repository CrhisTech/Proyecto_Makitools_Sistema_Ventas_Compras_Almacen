using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.DTOs.Responses
{
    public class CompraResponseDto
    {
        public int IdCompra { get; set; }
        public string NumeroOrdenCompra { get; set; } = null!;
        public string Proveedor { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string TipoDocumento { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public DateTime? FechaCompra { get; set; }
        public DateTime FechaEntregaEsperada { get; set; }
        public decimal MontoTotal { get; set; }
        public string Estado { get; set; } = null!;
    }
}
