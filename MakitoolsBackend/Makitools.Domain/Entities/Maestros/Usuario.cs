using Makitools.Domain.Entities.Almacen;
using Makitools.Domain.Entities.Compras;
using Makitools.Domain.Entities.Ventas;

namespace Makitools.Domain.Entities.Maestros
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Clave { get; set; } = null!;

        public bool? Reestablecer { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

        public virtual Rol IdRolNavigation { get; set; } = null!;

        public virtual ICollection<Kardex> Kardices { get; set; } = new List<Kardex>();

        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    }
}