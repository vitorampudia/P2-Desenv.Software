namespace P2_Desenv.Software.Models
{
    public class Treino
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int TreinadorId { get; set; }
        public Treinador Treinador { get; set; }
        public List<TreinoExercicio> TreinoExercicios { get; set; }

        public Treino(int alunoId, Aluno aluno, int treinadorId, List<TreinoExercicio> treinoExercicios)
        {
            AlunoId = alunoId;
            Aluno = aluno;
            TreinadorId = treinadorId;
            TreinoExercicios = treinoExercicios;
        }
        private Treino() { }
    }
}
