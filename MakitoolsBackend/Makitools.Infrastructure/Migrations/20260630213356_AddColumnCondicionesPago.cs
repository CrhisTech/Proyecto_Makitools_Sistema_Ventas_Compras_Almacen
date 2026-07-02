using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Makitools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCondicionesPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CondicionesPago",
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
                name: "CondicionesPago",
                schema: "Compras",
                table: "Compra");
        }
    }
}
