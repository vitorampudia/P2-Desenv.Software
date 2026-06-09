using System.Text.Json.Serialization;
namespace P2_Desenv.Software.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateOnly DataNascimento { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }

        [JsonIgnore]
        public Treinador Treinador { get; set; }
        public int TreinadorId { get; set; }

        public ICollection<Mensalidade> Mensalidades { get; set; }



        public Aluno(string nome, string cpf, DateOnly dataNascimento, float peso, float altura, int treinadorId)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Peso = peso;
            Altura = altura;
            TreinadorId = treinadorId;
        }
        private Aluno() { }
    }
}
