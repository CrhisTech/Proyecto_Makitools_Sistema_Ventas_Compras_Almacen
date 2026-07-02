using Makitools.Domain.Entities.Almacen;
using Makitools.Domain.Entities.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Domain.Entities.Ventas
{
    public class Carrito
    {

        public int IdCarrito { get; set; }

        public int IdCliente { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;

        public virtual Producto IdProductoNavigation { get; set; } = null!;

    }
}
