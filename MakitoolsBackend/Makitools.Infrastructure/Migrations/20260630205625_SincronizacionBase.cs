using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Makitools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SincronizacionBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.EnsureSchema(
        //        name: "Ventas");

        //    migrationBuilder.EnsureSchema(
        //        name: "Almacen");

        //    migrationBuilder.EnsureSchema(
        //        name: "Maestros");

        //    migrationBuilder.EnsureSchema(
        //        name: "Compras");

        //    migrationBuilder.CreateTable(
        //        name: "Categoria",
        //        schema: "Almacen",
        //        columns: table => new
        //        {
        //            IdCategoria = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
        //            FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Categori__A3C02A105610846F", x => x.IdCategoria);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Cliente",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdCliente = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            TipoDocumento = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
        //            NumeroDocumento = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
        //            Nombres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            Apellidos = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            Correo = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
        //            Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
        //            FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Cliente__D5946642B3F6818C", x => x.IdCliente);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Departamento",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdDepartamento = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
        //            Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Departam__787A433D9D57C0A5", x => x.IdDepartamento);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Marca",
        //        schema: "Almacen",
        //        columns: table => new
        //        {
        //            IdMarca = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
        //            FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Marca__4076A887FA83B06B", x => x.IdMarca);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Proveedor",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdProveedor = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            RUC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
        //            RazonSocial = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
        //            Contacto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
        //            Correo = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Proveedo__E8B631AFEB64FA9E", x => x.IdProveedor);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Rol",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdRol = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Rol__2A49584C3F6DBABC", x => x.IdRol);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Provincia",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdProvincia = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
        //            Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            IdDepartamento = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Provinci__EED74455BF2E5689", x => x.IdProvincia);
        //            table.ForeignKey(
        //                name: "FK__Provincia__IdDep__4BAC3F29",
        //                column: x => x.IdDepartamento,
        //                principalSchema: "Maestros",
        //                principalTable: "Departamento",
        //                principalColumn: "IdDepartamento");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Producto",
        //        schema: "Almacen",
        //        columns: table => new
        //        {
        //            IdProducto = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            SKU = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
        //            Nombre = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
        //            Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            IdCategoria = table.Column<int>(type: "int", nullable: false),
        //            IdMarca = table.Column<int>(type: "int", nullable: false),
        //            PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            CostoPromedio = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
        //            Stock = table.Column<double>(type: "float", nullable: true, defaultValue: 0.0),
        //            RutaImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
        //            FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Producto__09889210B253E7C3", x => x.IdProducto);
        //            table.ForeignKey(
        //                name: "FK__Producto__IdCate__6E01572D",
        //                column: x => x.IdCategoria,
        //                principalSchema: "Almacen",
        //                principalTable: "Categoria",
        //                principalColumn: "IdCategoria");
        //            table.ForeignKey(
        //                name: "FK__Producto__IdMarc__6EF57B66",
        //                column: x => x.IdMarca,
        //                principalSchema: "Almacen",
        //                principalTable: "Marca",
        //                principalColumn: "IdMarca");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Usuario",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdUsuario = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdRol = table.Column<int>(type: "int", nullable: false),
        //            Nombres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            Apellidos = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            Correo = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
        //            Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Reestablecer = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
        //            Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
        //            FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Usuario__5B65BF9713007378", x => x.IdUsuario);
        //            table.ForeignKey(
        //                name: "FK__Usuario__IdRol__5629CD9C",
        //                column: x => x.IdRol,
        //                principalSchema: "Maestros",
        //                principalTable: "Rol",
        //                principalColumn: "IdRol");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Distrito",
        //        schema: "Maestros",
        //        columns: table => new
        //        {
        //            IdDistrito = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
        //            Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            IdProvincia = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Distrito__DE8EED59D1BC0A13", x => x.IdDistrito);
        //            table.ForeignKey(
        //                name: "FK__Distrito__IdProv__4E88ABD4",
        //                column: x => x.IdProvincia,
        //                principalSchema: "Maestros",
        //                principalTable: "Provincia",
        //                principalColumn: "IdProvincia");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Carrito",
        //        schema: "Ventas",
        //        columns: table => new
        //        {
        //            IdCarrito = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdCliente = table.Column<int>(type: "int", nullable: false),
        //            IdProducto = table.Column<int>(type: "int", nullable: false),
        //            Cantidad = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Carrito__8B4A618CAA24421C", x => x.IdCarrito);
        //            table.ForeignKey(
        //                name: "FK__Carrito__IdClien__0F624AF8",
        //                column: x => x.IdCliente,
        //                principalSchema: "Maestros",
        //                principalTable: "Cliente",
        //                principalColumn: "IdCliente");
        //            table.ForeignKey(
        //                name: "FK__Carrito__IdProdu__10566F31",
        //                column: x => x.IdProducto,
        //                principalSchema: "Almacen",
        //                principalTable: "Producto",
        //                principalColumn: "IdProducto");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Compra",
        //        schema: "Compras",
        //        columns: table => new
        //        {
        //            IdCompra = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdProveedor = table.Column<int>(type: "int", nullable: false),
        //            IdUsuario = table.Column<int>(type: "int", nullable: false),
        //            TipoDocumento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValue: "Factura"),
        //            NumeroFactura = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            NumeroGuiaEntrada = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            FechaCompra = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
        //            MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Registrado")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Compra__0A5CDB5C9C54687A", x => x.IdCompra);
        //            table.ForeignKey(
        //                name: "FK__Compra__IdProvee__75A278F5",
        //                column: x => x.IdProveedor,
        //                principalSchema: "Maestros",
        //                principalTable: "Proveedor",
        //                principalColumn: "IdProveedor");
        //            table.ForeignKey(
        //                name: "FK__Compra__IdUsuari__76969D2E",
        //                column: x => x.IdUsuario,
        //                principalSchema: "Maestros",
        //                principalTable: "Usuario",
        //                principalColumn: "IdUsuario");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Kardex",
        //        schema: "Almacen",
        //        columns: table => new
        //        {
        //            IdKardex = table.Column<long>(type: "bigint", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdProducto = table.Column<int>(type: "int", nullable: false),
        //            IdUsuario = table.Column<int>(type: "int", nullable: false),
        //            FechaMovimiento = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
        //            TipoMovimiento = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
        //            Concepto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
        //            NumeroGuia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            IdReferencia = table.Column<int>(type: "int", nullable: true),
        //            Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            CostoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            SaldoFisico = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Kardex__BC1BA4009B903A6E", x => x.IdKardex);
        //            table.ForeignKey(
        //                name: "FK__Kardex__IdProduc__14270015",
        //                column: x => x.IdProducto,
        //                principalSchema: "Almacen",
        //                principalTable: "Producto",
        //                principalColumn: "IdProducto");
        //            table.ForeignKey(
        //                name: "FK__Kardex__IdUsuari__151B244E",
        //                column: x => x.IdUsuario,
        //                principalSchema: "Maestros",
        //                principalTable: "Usuario",
        //                principalColumn: "IdUsuario");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Venta",
        //        schema: "Ventas",
        //        columns: table => new
        //        {
        //            IdVenta = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdCliente = table.Column<int>(type: "int", nullable: false),
        //            IdUsuario = table.Column<int>(type: "int", nullable: true),
        //            IdDistrito = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
        //            TipoComprobante = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Boleta"),
        //            NumeroComprobante = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            NumeroOperacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            RutaVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            AgenciaEnvio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            NumeroGuiaEnvio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            FechaVenta = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
        //            SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            IGV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            MetodoPago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Completado")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Venta__BC1240BD126DE311", x => x.IdVenta);
        //            table.ForeignKey(
        //                name: "FK__Venta__IdCliente__01142BA1",
        //                column: x => x.IdCliente,
        //                principalSchema: "Maestros",
        //                principalTable: "Cliente",
        //                principalColumn: "IdCliente");
        //            table.ForeignKey(
        //                name: "FK__Venta__IdDistrit__02FC7413",
        //                column: x => x.IdDistrito,
        //                principalSchema: "Maestros",
        //                principalTable: "Distrito",
        //                principalColumn: "IdDistrito");
        //            table.ForeignKey(
        //                name: "FK__Venta__IdUsuario__02084FDA",
        //                column: x => x.IdUsuario,
        //                principalSchema: "Maestros",
        //                principalTable: "Usuario",
        //                principalColumn: "IdUsuario");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "DetalleCompra",
        //        schema: "Compras",
        //        columns: table => new
        //        {
        //            IdDetalleCompra = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdCompra = table.Column<int>(type: "int", nullable: false),
        //            IdProducto = table.Column<int>(type: "int", nullable: false),
        //            Cantidad = table.Column<int>(type: "int", nullable: false),
        //            CostoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__DetalleC__E046CCBB45F7C0D2", x => x.IdDetalleCompra);
        //            table.ForeignKey(
        //                name: "FK__DetalleCo__IdCom__7C4F7684",
        //                column: x => x.IdCompra,
        //                principalSchema: "Compras",
        //                principalTable: "Compra",
        //                principalColumn: "IdCompra");
        //            table.ForeignKey(
        //                name: "FK__DetalleCo__IdPro__7D439ABD",
        //                column: x => x.IdProducto,
        //                principalSchema: "Almacen",
        //                principalTable: "Producto",
        //                principalColumn: "IdProducto");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "DetalleVenta",
        //        schema: "Ventas",
        //        columns: table => new
        //        {
        //            IdDetalleVenta = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IdVenta = table.Column<int>(type: "int", nullable: false),
        //            IdProducto = table.Column<int>(type: "int", nullable: false),
        //            Cantidad = table.Column<int>(type: "int", nullable: false),
        //            PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__DetalleV__AAA5CEC2D634A448", x => x.IdDetalleVenta);
        //            table.ForeignKey(
        //                name: "FK__DetalleVe__IdPro__09A971A2",
        //                column: x => x.IdProducto,
        //                principalSchema: "Almacen",
        //                principalTable: "Producto",
        //                principalColumn: "IdProducto");
        //            table.ForeignKey(
        //                name: "FK__DetalleVe__IdVen__08B54D69",
        //                column: x => x.IdVenta,
        //                principalSchema: "Ventas",
        //                principalTable: "Venta",
        //                principalColumn: "IdVenta");
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Carrito_Cliente",
        //        schema: "Ventas",
        //        table: "Carrito",
        //        column: "IdCliente");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Carrito_IdProducto",
        //        schema: "Ventas",
        //        table: "Carrito",
        //        column: "IdProducto");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ_Cliente_Producto",
        //        schema: "Ventas",
        //        table: "Carrito",
        //        columns: new[] { "IdCliente", "IdProducto" },
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Cliente_NumDoc",
        //        schema: "Maestros",
        //        table: "Cliente",
        //        column: "NumeroDocumento");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Cliente__60695A1943859169",
        //        schema: "Maestros",
        //        table: "Cliente",
        //        column: "Correo",
        //        unique: true,
        //        filter: "[Correo] IS NOT NULL");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Cliente__A42025881AE1C1D5",
        //        schema: "Maestros",
        //        table: "Cliente",
        //        column: "NumeroDocumento",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Compra_FechaCompra",
        //        schema: "Compras",
        //        table: "Compra",
        //        column: "FechaCompra",
        //        descending: new bool[0]);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Compra_IdProveedor",
        //        schema: "Compras",
        //        table: "Compra",
        //        column: "IdProveedor");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Compra_IdUsuario",
        //        schema: "Compras",
        //        table: "Compra",
        //        column: "IdUsuario");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_DetalleCompra_IdCompra",
        //        schema: "Compras",
        //        table: "DetalleCompra",
        //        column: "IdCompra");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_DetalleCompra_IdProducto",
        //        schema: "Compras",
        //        table: "DetalleCompra",
        //        column: "IdProducto");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_DetalleVenta_IdProducto",
        //        schema: "Ventas",
        //        table: "DetalleVenta",
        //        column: "IdProducto");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_DetalleVenta_IdVenta",
        //        schema: "Ventas",
        //        table: "DetalleVenta",
        //        column: "IdVenta");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Distrito_IdProvincia",
        //        schema: "Maestros",
        //        table: "Distrito",
        //        column: "IdProvincia");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Kardex_IdUsuario",
        //        schema: "Almacen",
        //        table: "Kardex",
        //        column: "IdUsuario");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Kardex_Producto_Fecha",
        //        schema: "Almacen",
        //        table: "Kardex",
        //        columns: new[] { "IdProducto", "FechaMovimiento" },
        //        descending: new[] { false, true });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Producto_IdCategoria",
        //        schema: "Almacen",
        //        table: "Producto",
        //        column: "IdCategoria");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Producto_IdMarca",
        //        schema: "Almacen",
        //        table: "Producto",
        //        column: "IdMarca");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Producto_Nombre",
        //        schema: "Almacen",
        //        table: "Producto",
        //        column: "Nombre");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Producto_SKU",
        //        schema: "Almacen",
        //        table: "Producto",
        //        column: "SKU");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Producto__CA1ECF0DE253D26A",
        //        schema: "Almacen",
        //        table: "Producto",
        //        column: "SKU",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Proveedo__CAF3326B39B00619",
        //        schema: "Maestros",
        //        table: "Proveedor",
        //        column: "RUC",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Provincia_IdDepartamento",
        //        schema: "Maestros",
        //        table: "Provincia",
        //        column: "IdDepartamento");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Rol__75E3EFCFD6AF42AA",
        //        schema: "Maestros",
        //        table: "Rol",
        //        column: "Nombre",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Usuario_IdRol",
        //        schema: "Maestros",
        //        table: "Usuario",
        //        column: "IdRol");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Usuario__60695A1934903A4C",
        //        schema: "Maestros",
        //        table: "Usuario",
        //        column: "Correo",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Venta_FechaVenta",
        //        schema: "Ventas",
        //        table: "Venta",
        //        column: "FechaVenta",
        //        descending: new bool[0]);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Venta_IdCliente",
        //        schema: "Ventas",
        //        table: "Venta",
        //        column: "IdCliente");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Venta_IdDistrito",
        //        schema: "Ventas",
        //        table: "Venta",
        //        column: "IdDistrito");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Venta_IdUsuario",
        //        schema: "Ventas",
        //        table: "Venta",
        //        column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrito",
                schema: "Ventas");

            migrationBuilder.DropTable(
                name: "DetalleCompra",
                schema: "Compras");

            migrationBuilder.DropTable(
                name: "DetalleVenta",
                schema: "Ventas");

            migrationBuilder.DropTable(
                name: "Kardex",
                schema: "Almacen");

            migrationBuilder.DropTable(
                name: "Compra",
                schema: "Compras");

            migrationBuilder.DropTable(
                name: "Venta",
                schema: "Ventas");

            migrationBuilder.DropTable(
                name: "Producto",
                schema: "Almacen");

            migrationBuilder.DropTable(
                name: "Proveedor",
                schema: "Maestros");

            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "Maestros");

            migrationBuilder.DropTable(
                name: "Distrito",
                schema: "Maestros");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Maestros");

            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "Almacen");

            migrationBuilder.DropTable(
                name: "Marca",
                schema: "Almacen");

            migrationBuilder.DropTable(
                name: "Provincia",
                schema: "Maestros");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "Maestros");

            migrationBuilder.DropTable(
                name: "Departamento",
                schema: "Maestros");
        }
    }
}
