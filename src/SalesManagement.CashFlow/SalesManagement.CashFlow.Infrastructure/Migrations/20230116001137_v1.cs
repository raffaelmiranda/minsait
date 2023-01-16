using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalesManagement.CashFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CashFlow");

            migrationBuilder.CreateTable(
                name: "Relatorio",
                schema: "CashFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeArquivo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Caminho = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoLancamento",
                schema: "CashFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoEm = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()"),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLancamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LancamentoBancario",
                schema: "CashFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoEm = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()"),
                    Descricao = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TipoLancamentoId = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoBancario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LancamentoBancario_TipoLancamento_TipoLancamentoId",
                        column: x => x.TipoLancamentoId,
                        principalSchema: "CashFlow",
                        principalTable: "TipoLancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "CashFlow",
                table: "TipoLancamento",
                columns: new[] { "Id", "CriadoEm", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 15, 21, 11, 37, 403, DateTimeKind.Local).AddTicks(137), "Debit" },
                    { 2, new DateTime(2023, 1, 15, 21, 11, 37, 403, DateTimeKind.Local).AddTicks(179), "Credit" }
                });

            migrationBuilder.InsertData(
                schema: "CashFlow",
                table: "LancamentoBancario",
                columns: new[] { "Id", "Categoria", "CriadoEm", "Descricao", "TipoLancamentoId", "Valor" },
                values: new object[,]
                {
                    { 1, "categoria 01", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 01", 1, 100.0m },
                    { 2, "categoria 02", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 02", 1, 100000.0m },
                    { 3, "categoria 03", new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 03", 2, 200000.0m },
                    { 4, "categoria 04", new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 04", 2, 100000.0m },
                    { 5, "categoria 05", new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 05", 1, 100.0m },
                    { 6, "categoria 06", new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 06", 1, 100000.0m },
                    { 7, "categoria 07", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 07", 2, 200000.0m },
                    { 8, "categoria 08", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "descrição 08", 2, 100000.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoBancario_TipoLancamentoId",
                schema: "CashFlow",
                table: "LancamentoBancario",
                column: "TipoLancamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentoBancario",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "Relatorio",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "TipoLancamento",
                schema: "CashFlow");
        }
    }
}
