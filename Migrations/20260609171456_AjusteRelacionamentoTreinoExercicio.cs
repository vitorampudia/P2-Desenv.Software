using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_Desenv.Software.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoTreinoExercicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Treinador_Cref",
                table: "Treinador",
                column: "Cref",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Cpf",
                table: "Alunos",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Treinador_Cref",
                table: "Treinador");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_Cpf",
                table: "Alunos");
        }
    }
}
