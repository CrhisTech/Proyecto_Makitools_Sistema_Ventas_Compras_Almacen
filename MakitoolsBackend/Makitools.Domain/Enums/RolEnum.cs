

namespace Makitools.Domain.Enums
{
    public enum RolEnum
    {
        // Administración y Sistemas
        AdministradorGeneral = 1,

        // Área de Ventas
        Vendedor = 2,
        JefeVentas = 3,

        // Área de Compras
        AuxiliarCompras = 4,
        AsistenteCompras = 5,
        JefeCompras = 6,

        // Área de Almacén
        AuxiliarAlmacen = 7,
        AsistenteAlmacen = 8,
        JefeAlmacen = 9,

        // Externos (Para el carrito de compras y la web pública)
        ClienteWeb = 10
    }
}
