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
                name: "TipoLancamento",
                schema: "CashFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { 1, new DateTime(2023, 1, 14, 17, 20, 21, 478, DateTimeKind.Local).AddTicks(9638), "Debit" },
                    { 2, new DateTime(2023, 1, 14, 17, 20, 21, 478, DateTimeKind.Local).AddTicks(9688), "Credit" }
                });

            migrationBuilder.InsertData(
                schema: "CashFlow",
                table: "LancamentoBancario",
                columns: new[] { "Id", "Categoria", "CriadoEm", "Descricao", "TipoLancamentoId", "Valor" },
                values: new object[,]
                {
                    { 1, "Agua", new DateTime(2023, 1, 14, 17, 20, 21, 478, DateTimeKind.Local).AddTicks(9699), "Conta da Sabesp", 1, 100.0m },
                    { 2, "Folha de Pagamento", new DateTime(2023, 1, 14, 17, 20, 21, 478, DateTimeKind.Local).AddTicks(9710), "Despesa com funcionários", 1, 100000.0m },
                    { 3, "Recebivéis", new DateTime(2023, 1, 14, 17, 20, 21, 478, DateTimeKind.Local).AddTicks(9717), "Venda para client X", 2, 200000.0m },
                    { 4, "Recebivéis", new DateTime(2023, 1, 14, 17, 20, 21, 478, DateTimeKind.Local).AddTicks(9723), "Venda para client Y", 2, 100000.0m }
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
                name: "TipoLancamento",
                schema: "CashFlow");
        }
    }
}
