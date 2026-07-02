using System;
using System.Collections.Generic;
using Makitools.Domain.Entities.Compras;
using Makitools.Domain.Entities.Ventas;

namespace Makitools.Domain.Entities.Almacen
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Sku { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int IdCategoria { get; set; }

        public int IdMarca { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal? CostoPromedio { get; set; }

        public double? Stock { get; set; }

        public string? RutaImagen { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

        public virtual Marca IdMarcaNavigation { get; set; } = null!;

        public virtual ICollection<Kardex> Kardices { get; set; } = new List<Kardex>();
    }
}