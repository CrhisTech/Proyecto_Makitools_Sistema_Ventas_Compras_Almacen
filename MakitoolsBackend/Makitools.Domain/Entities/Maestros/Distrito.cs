using Makitools.Domain.Entities.Ventas;

namespace Makitools.Domain.Entities.Maestros
{
    public class Distrito
    {
        public string IdDistrito { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string IdProvincia { get; set; } = null!;

        public virtual Provincia IdProvinciaNavigation { get; set; } = null!;

        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}