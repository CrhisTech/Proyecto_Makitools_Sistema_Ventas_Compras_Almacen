using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.DTOs.Responses
{
    public class MarcaResponseDto
    {
        public int IdMarca { get; set; }
        public string Descripcion { get; set; } = null!;

        public bool? Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
