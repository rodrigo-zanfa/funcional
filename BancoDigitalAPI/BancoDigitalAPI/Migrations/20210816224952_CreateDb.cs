using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoDigitalAPI.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Correntistas",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correntistas", x => x.Cpf);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContasCorrentes",
                columns: table => new
                {
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Saldo = table.Column<double>(type: "double", nullable: false),
                    CorrentistaCpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasCorrentes", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_ContasCorrentes_Correntistas_CorrentistaCpf",
                        column: x => x.CorrentistaCpf,
                        principalTable: "Correntistas",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Correntistas",
                columns: new[] { "Cpf", "Nome" },
                values: new object[] { "32516143800", "Rodrigo Zanferrari" });

            migrationBuilder.InsertData(
                table: "Correntistas",
                columns: new[] { "Cpf", "Nome" },
                values: new object[] { "33713557802", "Raphaela Goes" });

            migrationBuilder.InsertData(
                table: "ContasCorrentes",
                columns: new[] { "Numero", "CorrentistaCpf", "Saldo" },
                values: new object[] { 1L, "32516143800", 0.0 });

            migrationBuilder.InsertData(
                table: "ContasCorrentes",
                columns: new[] { "Numero", "CorrentistaCpf", "Saldo" },
                values: new object[] { 3L, "32516143800", 0.0 });

            migrationBuilder.InsertData(
                table: "ContasCorrentes",
                columns: new[] { "Numero", "CorrentistaCpf", "Saldo" },
                values: new object[] { 2L, "33713557802", 0.0 });

            migrationBuilder.CreateIndex(
                name: "IX_ContasCorrentes_CorrentistaCpf",
                table: "ContasCorrentes",
                column: "CorrentistaCpf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContasCorrentes");

            migrationBuilder.DropTable(
                name: "Correntistas");
        }
    }
}
