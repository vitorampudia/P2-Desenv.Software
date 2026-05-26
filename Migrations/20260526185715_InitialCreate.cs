using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_Desenv.Software.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    GrupoMuscular = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treinador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cref = table.Column<string>(type: "TEXT", nullable: false),
                    Especializacao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Peso = table.Column<float>(type: "REAL", nullable: false),
                    Altura = table.Column<float>(type: "REAL", nullable: false),
                    TreinadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Treinador_TreinadorId",
                        column: x => x.TreinadorId,
                        principalTable: "Treinador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensalidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoPagamento = table.Column<int>(type: "INTEGER", nullable: true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensalidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensalidade_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TreinadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treino_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treino_Treinador_TreinadorId",
                        column: x => x.TreinadorId,
                        principalTable: "Treinador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreinoExercicios",
                columns: table => new
                {
                    TreinoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExercicioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Carga = table.Column<float>(type: "REAL", nullable: false),
                    QtdRepeticoes = table.Column<float>(type: "REAL", nullable: false),
                    QtdSeries = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoExercicios", x => new { x.TreinoId, x.ExercicioId });
                    table.ForeignKey(
                        name: "FK_TreinoExercicios_Exercicio_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "Exercicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreinoExercicios_Treino_TreinoId",
                        column: x => x.TreinoId,
                        principalTable: "Treino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TreinadorId",
                table: "Alunos",
                column: "TreinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidade_AlunoId",
                table: "Mensalidade",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Treino_AlunoId",
                table: "Treino",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Treino_TreinadorId",
                table: "Treino",
                column: "TreinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoExercicios_ExercicioId",
                table: "TreinoExercicios",
                column: "ExercicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensalidade");

            migrationBuilder.DropTable(
                name: "TreinoExercicios");

            migrationBuilder.DropTable(
                name: "Exercicio");

            migrationBuilder.DropTable(
                name: "Treino");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Treinador");
        }
    }
}
