using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Clave { get; set; } = null!;
    }
}
