namespace P2_Desenv.Software.Models
{
    public class Exercicio
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string GrupoMuscular { get; set; }

        public string Descricao { get; set; }

        public Exercicio(string nome, string grupoMuscular, string descricao)
        {
            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Descricao = descricao;
        }
        private Exercicio() { }
    }
}
