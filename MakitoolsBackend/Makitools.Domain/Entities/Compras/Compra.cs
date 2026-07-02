

using Makitools.Domain.Entities.Maestros;
using Makitools.Domain.Enums;

namespace Makitools.Domain.Entities.Compras
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }


        public string NumeroOrdenCompra { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public string CondicionesPago { get; set; } = null!;
        public DateTime FechaEntregaEsperada { get; set; }
        public string LugarEntrega { get; set; } = null!;


        public string TipoDocumento { get; set; } = null!;
        public string? NumeroFactura { get; set; }
        public string? NumeroGuiaEntrada { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaCompra { get; set; }
        public decimal MontoTotal { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
        public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    }
}
