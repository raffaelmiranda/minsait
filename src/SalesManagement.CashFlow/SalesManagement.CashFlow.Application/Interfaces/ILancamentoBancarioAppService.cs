using SalesManagement.CashFlow.Application.Models;
using SalesManagement.CashFlow.Domain.Entities;

namespace SalesManagement.CashFlow.Application.Interfaces
{
    public interface ILancamentoBancarioAppService : IBaseAppService<LancamentoBancario>
    {
        List<LancamentoBancarioObter> ObterTodosComTipoLancamento();
    }
}
