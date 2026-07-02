using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.DTOs.Responses
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public bool? Activo { get;  set; }
        public int IdRol { get;  set; }
    }
}
