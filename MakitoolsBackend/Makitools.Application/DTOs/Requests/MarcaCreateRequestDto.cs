using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class MarcaCreateRequestDto
    {
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(100)]
        public string Descripcion { get; set; } = null!;

    }
}
