using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreProjetoFinal.Migrations
{
    public partial class ajusteJogoPlataforma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JogoPlataforma_Plataformas_EstudioId",
                table: "JogoPlataforma");

            migrationBuilder.RenameColumn(
                name: "EstudioId",
                table: "JogoPlataforma",
                newName: "PlataformaId");

            migrationBuilder.RenameIndex(
                name: "IX_JogoPlataforma_EstudioId",
                table: "JogoPlataforma",
                newName: "IX_JogoPlataforma_PlataformaId");

            migrationBuilder.AddForeignKey(
                name: "FK_JogoPlataforma_Plataformas_PlataformaId",
                table: "JogoPlataforma",
                column: "PlataformaId",
                principalTable: "Plataformas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JogoPlataforma_Plataformas_PlataformaId",
                table: "JogoPlataforma");

            migrationBuilder.RenameColumn(
                name: "PlataformaId",
                table: "JogoPlataforma",
                newName: "EstudioId");

            migrationBuilder.RenameIndex(
                name: "IX_JogoPlataforma_PlataformaId",
                table: "JogoPlataforma",
                newName: "IX_JogoPlataforma_EstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_JogoPlataforma_Plataformas_EstudioId",
                table: "JogoPlataforma",
                column: "EstudioId",
                principalTable: "Plataformas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
