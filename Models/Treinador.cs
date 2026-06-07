using System.Text.Json.Serialization;
namespace P2_Desenv.Software.Models
{
    public class Treinador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cref { get; set; }
        public string Especializacao { get; set; }

        [JsonIgnore]
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

        public Treinador(string nome, string cref, string especializacao)
        {
            Nome = nome;
            Cref = cref;
            Especializacao = especializacao;
        }
        private Treinador() { }
    }
}
