using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class ProductoUpdateRequestDto
    {
        [Required(ErrorMessage = "El SKU es obligatorio.")]
        [StringLength(50)]
        public string Sku { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a 0.")]
        public decimal PrecioVenta { get; set; }

        public IFormFile? Imagen { get; set; }
    }
}

