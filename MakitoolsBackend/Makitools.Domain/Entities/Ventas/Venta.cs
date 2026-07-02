

using Makitools.Domain.Entities.Maestros;
using System.ComponentModel.DataAnnotations;

namespace Makitools.Domain.Entities.Ventas
{
    public class Venta
    {

        public int IdVenta { get; set; }

        public int IdCliente { get; set; }

        public int? IdUsuario { get; set; }

        public string? IdDistrito { get; set; }

        public string TipoComprobante { get; set; } = null!;

        public string? NumeroComprobante { get; set; }

        public string? NumeroOperacion { get; set; }

        public string? RutaVoucher { get; set; }

        public string? AgenciaEnvio { get; set; }

        public string? NumeroGuiaEnvio { get; set; }

        public DateTime? FechaVenta { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Igv { get; set; }

        public decimal MontoTotal { get; set; }

        public string? MetodoPago { get; set; }

        public string? Estado { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

        public virtual Cliente IdClienteNavigation { get; set; } = null!;

        public virtual Distrito? IdDistritoNavigation { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }

    }
}
