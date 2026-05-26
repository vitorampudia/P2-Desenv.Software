using P2_Desenv.Software.Enums;

namespace P2_Desenv.Software.Models
{
    public class Mensalidade
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public StatusMensalidade Status { get; set; }
        public TipoPagamento? TipoPagamento { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public Mensalidade(decimal valor, DateTime dataVencimento, DateTime? dataPagamento, StatusMensalidade status, TipoPagamento? tipoPagamento, int alunoId)
        {
            Valor = valor;
            DataVencimento = dataVencimento;
            DataPagamento = dataPagamento;
            Status = status;
            TipoPagamento = tipoPagamento;
            AlunoId = alunoId;
        }
        private Mensalidade() { }
    }
}
