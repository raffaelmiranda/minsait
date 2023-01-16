using SalesManagement.Report.Domain.Entities;

namespace SalesManagement.Report.Domain.Interfaces.Repositories
{
    public interface ILancamentoBancarioRepository : IBaseRepository<LancamentoBancario>
    {
        List<LancamentoBancario> ObterTodosComTipoLancamento();
    }
}
