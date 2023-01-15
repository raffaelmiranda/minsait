namespace SalesManagement.CashFlow.Domain.Entities
{
    public class TipoLancamento
    {
        public TipoLancamento() {  }
        public TipoLancamento(int id, string name, LancamentoBancario? bankStatement = null)
        {
            Id = id;
            CriadoEm = DateTime.Now;
            Nome = name;
            LancamentoBancario = bankStatement;

        }
        public int Id { get; private set; }
        public DateTime CriadoEm { get; private  set; }
        public string Nome { get; private set; } = string.Empty;
        public LancamentoBancario? LancamentoBancario { get; private set; }
    }
}
