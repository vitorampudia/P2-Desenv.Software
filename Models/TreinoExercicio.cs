using P2_Desenv_Software.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace P2_Desenv_Software.Models
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

        public TreinoExercicio() { }
        public TreinoExercicio(int treinoId, Treino treino, int exercicioId, Exercicio exercicio, float carga, float qtdRepeticoes, float qtdSeries) 
        {
            TreinoId = treinoId;
            Treino = treino;
            Exercicio = exercicio;
            ExercicioId = exercicioId;
            Carga = carga;
            QtdRepeticoes = qtdRepeticoes;
            QtdSeries = qtdSeries;
        }

    }
}
