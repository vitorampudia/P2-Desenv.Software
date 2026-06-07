using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_Desenv.Software.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreinoExercicios_Exercicio_ExercicioId",
                table: "TreinoExercicios");

            migrationBuilder.DropTable(
                name: "Exercicio");

            migrationBuilder.CreateTable(
                name: "Exercicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    GrupoMuscular = table.Column<string>(type: "TEXT", nullable: false),
                    Grupo = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TreinoExercicios_Exercicios_ExercicioId",
                table: "TreinoExercicios",
                column: "ExercicioId",
                principalTable: "Exercicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreinoExercicios_Exercicios_ExercicioId",
                table: "TreinoExercicios");

            migrationBuilder.DropTable(
                name: "Exercicios");

            migrationBuilder.CreateTable(
                name: "Exercicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    GrupoMuscular = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicio", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TreinoExercicios_Exercicio_ExercicioId",
                table: "TreinoExercicios",
                column: "ExercicioId",
                principalTable: "Exercicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
