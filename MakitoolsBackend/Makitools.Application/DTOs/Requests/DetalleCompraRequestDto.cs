using System.ComponentModel.DataAnnotations;

namespace Makitools.Application.DTOs.Requests
{
    public class DetalleCompraRequestDto
    {
        [Required]
        public int IdProducto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo unitario debe ser mayor a 0.")]
        public decimal CostoUnitario { get; set; }
    }
}