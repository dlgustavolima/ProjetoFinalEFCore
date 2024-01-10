using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreProjetoFinal.Migrations
{
    public partial class NovoMapeamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstudioJogo_Estudios_EstudioId",
                table: "EstudioJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_EstudioJogo_Jogos_JogosId",
                table: "EstudioJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneroJogo_Generos_GeneroId",
                table: "GeneroJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneroJogo_Jogos_JogosId",
                table: "GeneroJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoPlataforma_Jogos_JogosId",
                table: "JogoPlataforma");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoPlataforma_Plataformas_PlataformaId",
                table: "JogoPlataforma");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Codigos_CodigoAcessoId",
                table: "Jogos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneroJogo",
                table: "GeneroJogo");

            migrationBuilder.DropIndex(
                name: "IX_GeneroJogo_JogosId",
                table: "GeneroJogo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstudioJogo",
                table: "EstudioJogo");

            migrationBuilder.DropIndex(
                name: "IX_EstudioJogo_JogosId",
                table: "EstudioJogo");

            migrationBuilder.RenameColumn(
                name: "PlataformaId",
                table: "JogoPlataforma",
                newName: "EstudioId");

            migrationBuilder.RenameIndex(
                name: "IX_JogoPlataforma_PlataformaId",
                table: "JogoPlataforma",
                newName: "IX_JogoPlataforma_EstudioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneroJogo",
                table: "GeneroJogo",
                columns: new[] { "JogosId", "GeneroId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstudioJogo",
                table: "EstudioJogo",
                columns: new[] { "JogosId", "EstudioId" });

            migrationBuilder.CreateIndex(
                name: "IX_GeneroJogo_GeneroId",
                table: "GeneroJogo",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudioJogo_EstudioId",
                table: "EstudioJogo",
                column: "EstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstudioJogo_Estudios_EstudioId",
                table: "EstudioJogo",
                column: "EstudioId",
                principalTable: "Estudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EstudioJogo_Jogos_JogosId",
                table: "EstudioJogo",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroJogo_Generos_GeneroId",
                table: "GeneroJogo",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroJogo_Jogos_JogosId",
                table: "GeneroJogo",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoPlataforma_Jogos_JogosId",
                table: "JogoPlataforma",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoPlataforma_Plataformas_EstudioId",
                table: "JogoPlataforma",
                column: "EstudioId",
                principalTable: "Plataformas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Codigos_CodigoAcessoId",
                table: "Jogos",
                column: "CodigoAcessoId",
                principalTable: "Codigos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstudioJogo_Estudios_EstudioId",
                table: "EstudioJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_EstudioJogo_Jogos_JogosId",
                table: "EstudioJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneroJogo_Generos_GeneroId",
                table: "GeneroJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneroJogo_Jogos_JogosId",
                table: "GeneroJogo");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoPlataforma_Jogos_JogosId",
                table: "JogoPlataforma");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoPlataforma_Plataformas_EstudioId",
                table: "JogoPlataforma");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Codigos_CodigoAcessoId",
                table: "Jogos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneroJogo",
                table: "GeneroJogo");

            migrationBuilder.DropIndex(
                name: "IX_GeneroJogo_GeneroId",
                table: "GeneroJogo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstudioJogo",
                table: "EstudioJogo");

            migrationBuilder.DropIndex(
                name: "IX_EstudioJogo_EstudioId",
                table: "EstudioJogo");

            migrationBuilder.RenameColumn(
                name: "EstudioId",
                table: "JogoPlataforma",
                newName: "PlataformaId");

            migrationBuilder.RenameIndex(
                name: "IX_JogoPlataforma_EstudioId",
                table: "JogoPlataforma",
                newName: "IX_JogoPlataforma_PlataformaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneroJogo",
                table: "GeneroJogo",
                columns: new[] { "GeneroId", "JogosId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstudioJogo",
                table: "EstudioJogo",
                columns: new[] { "EstudioId", "JogosId" });

            migrationBuilder.CreateIndex(
                name: "IX_GeneroJogo_JogosId",
                table: "GeneroJogo",
                column: "JogosId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudioJogo_JogosId",
                table: "EstudioJogo",
                column: "JogosId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstudioJogo_Estudios_EstudioId",
                table: "EstudioJogo",
                column: "EstudioId",
                principalTable: "Estudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstudioJogo_Jogos_JogosId",
                table: "EstudioJogo",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroJogo_Generos_GeneroId",
                table: "GeneroJogo",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroJogo_Jogos_JogosId",
                table: "GeneroJogo",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoPlataforma_Jogos_JogosId",
                table: "JogoPlataforma",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoPlataforma_Plataformas_PlataformaId",
                table: "JogoPlataforma",
                column: "PlataformaId",
                principalTable: "Plataformas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Codigos_CodigoAcessoId",
                table: "Jogos",
                column: "CodigoAcessoId",
                principalTable: "Codigos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
