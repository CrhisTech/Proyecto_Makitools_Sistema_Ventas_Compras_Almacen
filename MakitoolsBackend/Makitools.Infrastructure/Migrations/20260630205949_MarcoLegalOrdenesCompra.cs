using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Makitools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MarcoLegalOrdenesCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                schema: "Maestros",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntregaEsperada",
                schema: "Compras",
                table: "Compra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LugarEntrega",
                schema: "Compras",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                schema: "Compras",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroOrdenCompra",
                schema: "Compras",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                schema: "Maestros",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "FechaEntregaEsperada",
                schema: "Compras",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "LugarEntrega",
                schema: "Compras",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "Moneda",
                schema: "Compras",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "NumeroOrdenCompra",
                schema: "Compras",
                table: "Compra");
        }
    }
}
