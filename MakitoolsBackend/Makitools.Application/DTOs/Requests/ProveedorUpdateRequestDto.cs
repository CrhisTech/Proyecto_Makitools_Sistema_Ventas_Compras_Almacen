using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class ProveedorUpdateRequestDto
    {
        [Required(ErrorMessage = "El RUC es obligatorio.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener exactamente 11 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El RUC solo debe contener números.")]
        public string Ruc { get; set; } = null!;

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        [StringLength(200, ErrorMessage = "La razón social no puede exceder los 200 caracteres.")]
        public string RazonSocial { get; set; } = null!;

        [StringLength(150, ErrorMessage = "El nombre del contacto no puede exceder los 150 caracteres.")]
        public string? Contacto { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string? Correo { get; set; }
    }
}
