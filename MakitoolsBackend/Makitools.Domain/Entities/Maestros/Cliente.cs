

using Makitools.Domain.Entities.Ventas;

namespace Makitools.Domain.Entities.Maestros
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string TipoDocumento { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string Nombres { get; set; } = null!;

        public string? Apellidos { get; set; }

        public string? Correo { get; set; }

        public string? Clave { get; set; }

        public string? Telefono { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public bool? Activo { get; set; }

        public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    }
}
