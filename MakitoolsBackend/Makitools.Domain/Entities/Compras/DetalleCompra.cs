

using Makitools.Domain.Entities.Almacen;

namespace Makitools.Domain.Entities.Compras
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }

        public int IdCompra { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal CostoUnitario { get; set; }

        public decimal Total { get; set; }

        public virtual Compra IdCompraNavigation { get; set; } = null!;

        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
