using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.DTOs.Responses
{
    public class ProveedorResponseDto
    {
        public int IdProveedor { get; set; }

        public string Ruc { get; set; } = null!;

        public string RazonSocial { get; set; } = null!;

        public string? Contacto { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public bool Activo { get; set; }
    }
}
