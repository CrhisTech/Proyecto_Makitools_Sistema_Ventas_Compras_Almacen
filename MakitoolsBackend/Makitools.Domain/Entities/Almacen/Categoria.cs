using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Makitools.Domain.Entities.Almacen
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        public string Descripcion { get; set; } = null!;

        public bool? Activo { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    }
}
