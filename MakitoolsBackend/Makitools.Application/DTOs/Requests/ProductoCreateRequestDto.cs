using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class ProductoCreateRequestDto
    {
        [Required(ErrorMessage = "El SKU es obligatorio.")]
        [StringLength(50, ErrorMessage = "El SKU no puede exceder los 50 caracteres.")]
        public string Sku { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre del producto no puede exceder los 150 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una marca válida.")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a 0.")]
        public decimal PrecioVenta { get; set; }
        [Required(ErrorMessage = "Debe insertar una imagen para el producto.")]

        public IFormFile? Imagen { get; set; }
    }
}
