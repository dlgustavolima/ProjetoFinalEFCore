using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreProjetoFinal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Codigos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(25)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Empresa = table.Column<string>(type: "varchar(100)", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plataformas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataformas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: true),
                    PlataformaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoAcessoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogos_Codigos_CodigoAcessoId",
                        column: x => x.CodigoAcessoId,
                        principalTable: "Codigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstudioJogo",
                columns: table => new
                {
                    EstudioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudioJogo", x => new { x.EstudioId, x.JogosId });
                    table.ForeignKey(
                        name: "FK_EstudioJogo_Estudios_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "Estudios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudioJogo_Jogos_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneroJogo",
                columns: table => new
                {
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroJogo", x => new { x.GeneroId, x.JogosId });
                    table.ForeignKey(
                        name: "FK_GeneroJogo_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroJogo_Jogos_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JogoPlataforma",
                columns: table => new
                {
                    JogosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlataformaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoPlataforma", x => new { x.JogosId, x.PlataformaId });
                    table.ForeignKey(
                        name: "FK_JogoPlataforma_Jogos_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogoPlataforma_Plataformas_PlataformaId",
                        column: x => x.PlataformaId,
                        principalTable: "Plataformas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstudioJogo_JogosId",
                table: "EstudioJogo",
                column: "JogosId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroJogo_JogosId",
                table: "GeneroJogo",
                column: "JogosId");

            migrationBuilder.CreateIndex(
                name: "IX_JogoPlataforma_PlataformaId",
                table: "JogoPlataforma",
                column: "PlataformaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_CodigoAcessoId",
                table: "Jogos",
                column: "CodigoAcessoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstudioJogo");

            migrationBuilder.DropTable(
                name: "GeneroJogo");

            migrationBuilder.DropTable(
                name: "JogoPlataforma");

            migrationBuilder.DropTable(
                name: "Estudios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Plataformas");

            migrationBuilder.DropTable(
                name: "Codigos");
        }
    }
}
