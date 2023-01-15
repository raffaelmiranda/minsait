namespace SalesManagement.CashFlow.Application.Models
{
    public class LancamentoBancarioInserir
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int TipoLancamentoId { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
