namespace P2_Desenv.Software.Models
{
    public class TreinoExercicio
    {
        public int TreinoId { get; set; }
        public Treino Treino { get; set; }
        public int ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }
        public float Carga { get; set; }
        public float QtdRepeticoes { get; set; }
        public float QtdSeries { get; set; }
    }
}
