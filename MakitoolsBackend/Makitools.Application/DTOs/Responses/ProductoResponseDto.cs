using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.DTOs.Responses
{
    public class ProductoResponseDto
    {
        public int IdProducto { get; set; }
        public string SKU { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public decimal PrecioVenta { get; set; } 
        public int Stock { get; set; }
        public string RutaImagen { get; set; } = null!;
        public bool? Activo { get; set; }

    }
}
