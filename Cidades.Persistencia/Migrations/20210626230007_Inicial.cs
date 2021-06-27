using Microsoft.EntityFrameworkCore.Migrations;

namespace Cidades.Persistencia.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    NumeroHabitantes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fronteiras",
                columns: table => new
                {
                    CidadeOrigemId = table.Column<int>(type: "INTEGER", nullable: false),
                    CidadeFronteiraId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fronteiras", x => new { x.CidadeOrigemId, x.CidadeFronteiraId });
                    table.ForeignKey(
                        name: "FK_Fronteiras_Cidades_CidadeFronteiraId",
                        column: x => x.CidadeFronteiraId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fronteiras_Cidades_CidadeOrigemId",
                        column: x => x.CidadeOrigemId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fronteiras_CidadeFronteiraId",
                table: "Fronteiras",
                column: "CidadeFronteiraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fronteiras");

            migrationBuilder.DropTable(
                name: "Cidades");
        }
    }
}
