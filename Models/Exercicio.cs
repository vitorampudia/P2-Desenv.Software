using P2_Desenv.Software.Enums;
using P2_Desenv_Software.Models;
using System.ComponentModel.DataAnnotations;

public class Exercicio
{
    [Key]
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string GrupoMuscular { get; set; } = string.Empty;

    public GrupoMuscular Grupo { get; set; }

    public string? Descricao { get; set; }

    public List<TreinoExercicio> TreinoExercicios { get; set; } = new();

    public Exercicio() { }

    public Exercicio(string nome, string grupoMuscular, string? descricao = null)
    {
        Nome = nome;
        GrupoMuscular = grupoMuscular;
        Descricao = descricao;
    }
}