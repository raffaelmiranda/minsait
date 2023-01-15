using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.CashFlow.Application.Models
{
    public class LancamentoBancarioObter
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int TipoLancamentoId { get; set; }
        public string TipoLancamento { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
