using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Makitools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateSpGenerarCorrelativoCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
            CREATE PROCEDURE [Compras].[sp_GenerarNumeroOrdenCompra]
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @UltimoNumero INT;
                DECLARE @NuevoCorrelativo VARCHAR(20);

                SELECT @UltimoNumero = ISNULL(MAX(TRY_CAST(SUBSTRING(NumeroOrdenCompra, 4, LEN(NumeroOrdenCompra)) AS INT)), 0)
                FROM [Compras].[Compra] WITH (UPDLOCK, HOLDLOCK)
                WHERE NumeroOrdenCompra LIKE 'OC-%';

                
                SET @NuevoCorrelativo = 'OC-' + RIGHT('000000' + CAST(@UltimoNumero + 1 AS VARCHAR(10)), 6);

                -- 3. Devolvemos el correlativo generado
                SELECT @NuevoCorrelativo AS NumeroGenerado;
            END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Compras].[sp_GenerarNumeroOrdenCompra]");
        }
    }
}
