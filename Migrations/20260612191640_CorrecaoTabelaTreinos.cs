using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_Desenv.Software.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTabelaTreinos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treino_Alunos_AlunoId",
                table: "Treino");

            migrationBuilder.DropForeignKey(
                name: "FK_Treino_Treinador_TreinadorId",
                table: "Treino");

            migrationBuilder.DropForeignKey(
                name: "FK_TreinoExercicios_Treino_TreinoId",
                table: "TreinoExercicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treino",
                table: "Treino");

            migrationBuilder.RenameTable(
                name: "Treino",
                newName: "Treinos");

            migrationBuilder.RenameIndex(
                name: "IX_Treino_TreinadorId",
                table: "Treinos",
                newName: "IX_Treinos_TreinadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Treino_AlunoId",
                table: "Treinos",
                newName: "IX_Treinos_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treinos",
                table: "Treinos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreinoExercicios_Treinos_TreinoId",
                table: "TreinoExercicios",
                column: "TreinoId",
                principalTable: "Treinos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Alunos_AlunoId",
                table: "Treinos",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Treinador_TreinadorId",
                table: "Treinos",
                column: "TreinadorId",
                principalTable: "Treinador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreinoExercicios_Treinos_TreinoId",
                table: "TreinoExercicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Alunos_AlunoId",
                table: "Treinos");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Treinador_TreinadorId",
                table: "Treinos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treinos",
                table: "Treinos");

            migrationBuilder.RenameTable(
                name: "Treinos",
                newName: "Treino");

            migrationBuilder.RenameIndex(
                name: "IX_Treinos_TreinadorId",
                table: "Treino",
                newName: "IX_Treino_TreinadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Treinos_AlunoId",
                table: "Treino",
                newName: "IX_Treino_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treino",
                table: "Treino",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treino_Alunos_AlunoId",
                table: "Treino",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treino_Treinador_TreinadorId",
                table: "Treino",
                column: "TreinadorId",
                principalTable: "Treinador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreinoExercicios_Treino_TreinoId",
                table: "TreinoExercicios",
                column: "TreinoId",
                principalTable: "Treino",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
