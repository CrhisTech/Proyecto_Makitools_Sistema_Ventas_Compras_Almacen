using System.Collections.Generic;

namespace Makitools.Domain.Entities.Maestros
{
    public class Departamento
    {
        public string IdDepartamento { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
    }
}
