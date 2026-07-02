using Makitools.Domain.Entities.Maestros;

namespace Makitools.Domain.Entities.Almacen
{
    public class Kardex
    {
        public long IdKardex { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

        public DateTime? FechaMovimiento { get; set; }

        public string TipoMovimiento { get; set; } = null!;

        public string Concepto { get; set; } = null!;

        public string? NumeroGuia { get; set; }

        public int? IdReferencia { get; set; }

        public decimal Cantidad { get; set; }

        public decimal CostoUnitario { get; set; }

        public decimal SaldoFisico { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
