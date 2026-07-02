using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class ClienteUpdateRequestDto
    {
        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public string TipoDocumento { get; set; } = null!;

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        public string NumeroDocumento { get; set; } = null!;

        [Required(ErrorMessage = "Los nombres o razón social son obligatorios.")]
        public string Nombres { get; set; } = null!;

        public string? Apellidos { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? Clave { get; set; } 
    }
}
