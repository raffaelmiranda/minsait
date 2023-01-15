using SalesManagement.CashFlow.Domain.Entities;

namespace SalesManagement.CashFlow.Domain.Interfaces.Repositories
{
    public interface ILancamentoBancarioRepository : IBaseRepository<LancamentoBancario>
    {
        List<LancamentoBancario> ObterTodosComTipoLancamento();
    }
}
