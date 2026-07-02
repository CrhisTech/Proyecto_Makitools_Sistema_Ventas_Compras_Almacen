
using Makitools.Domain.Entities.Compras;

namespace Makitools.Domain.Entities.Maestros
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }

        public string Ruc { get; set; } = null!;

        public string RazonSocial { get; set; } = null!;

        public string? Contacto { get; set; }
        public string? Direccion {  get; set; }
        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public bool? Activo { get; set; }

        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    }
}
