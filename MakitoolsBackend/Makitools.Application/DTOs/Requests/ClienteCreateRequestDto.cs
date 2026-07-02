using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class ClienteCreateRequestDto
    {
        [Required(ErrorMessage = "El tipo de documento es obligatorio (ej. DNI, RUC, CE).")]
        [StringLength(20)]
        public string TipoDocumento { get; set; } = null!;

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de documento no puede exceder los 20 caracteres.")]
        public string NumeroDocumento { get; set; } = null!;

        [Required(ErrorMessage = "Los nombres o razón social son obligatorios.")]
        [StringLength(150, ErrorMessage = "Los nombres no pueden exceder los 150 caracteres.")]
        public string Nombres { get; set; } = null!;

        [StringLength(150, ErrorMessage = "Los apellidos no pueden exceder los 150 caracteres.")]
        public string? Apellidos { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(150)]
        public string? Correo { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria para el acceso web.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Clave { get; set; } = null!;
    }
}
