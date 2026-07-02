using Makitools.Domain.Entities.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Domain.Entities.Ventas
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Descuento { get; set; }

        public decimal Total { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;

        public virtual Venta IdVentaNavigation { get; set; } = null!;

    }
}
