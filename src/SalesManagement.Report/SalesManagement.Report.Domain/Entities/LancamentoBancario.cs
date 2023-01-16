namespace SalesManagement.Report.Domain.Entities
{
    public class LancamentoBancario
    {
        public LancamentoBancario() { }

        public LancamentoBancario(
            string descricao,
            decimal valor,
            int tipoLancamentoId,
            string categoria,
            int? id = null,
            TipoLancamento? tipoLancamento = null
            )
        {
            Id = id;
            CriadoEm = DateTime.Now;
            Descricao = descricao;
            Valor = valor;
            TipoLancamentoId = tipoLancamentoId;
            Categoria = categoria;
            TipoLancamento = tipoLancamento;
        }

        public int? Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public int TipoLancamentoId { get; private set; }
        public TipoLancamento? TipoLancamento { get; private set; }
        public string Categoria { get; private set; } = string.Empty;

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetValor(decimal valor)
        {
            Valor = valor;
        }
    }
}
