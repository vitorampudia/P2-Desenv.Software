using P2_Desenv.Software.Enums;
using System.ComponentModel.DataAnnotations;
using System;

namespace P2_Desenv_Software.Models
{
    public class Exercicio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O grupo muscular é obrigatório.")]
        public string GrupoMuscular { get; set; } = string.Empty;
        public GrupoMuscular Grupo { get; set; } // Enum do grupo muscular

        public string? Descricao { get; set; }

        public Exercicio(string nome, string grupoMuscular, string? descricao = null)
        {
            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Descricao = descricao;
        }
    }
}