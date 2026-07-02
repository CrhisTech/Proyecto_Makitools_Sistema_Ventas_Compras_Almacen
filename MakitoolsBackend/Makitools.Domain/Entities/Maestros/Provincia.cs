using System.Collections.Generic;

namespace Makitools.Domain.Entities.Maestros
{
    public class Provincia
    {

        public string IdProvincia { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string IdDepartamento { get; set; } = null!;

        public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    }
}