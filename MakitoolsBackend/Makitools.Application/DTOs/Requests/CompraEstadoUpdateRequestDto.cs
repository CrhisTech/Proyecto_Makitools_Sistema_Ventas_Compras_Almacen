using Makitools.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class CompraEstadoUpdateRequestDto
    {
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; } = null!; 
    }
}
