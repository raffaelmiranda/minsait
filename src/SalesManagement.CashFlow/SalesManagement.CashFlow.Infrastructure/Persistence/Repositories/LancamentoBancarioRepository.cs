using Microsoft.EntityFrameworkCore;
using SalesManagement.CashFlow.Domain.Entities;
using SalesManagement.CashFlow.Domain.Interfaces.Repositories;
using SalesManagement.CashFlow.Infrastructure.Persistence.Contexts;

namespace SalesManagement.CashFlow.Infrastructure.Persistence.Repositories
{
    public class LancamentoBancarioRepository : BaseRepository<LancamentoBancario>, ILancamentoBancarioRepository
    {
        public LancamentoBancarioRepository(CashFlowContext context) : base(context){ }

        public List<LancamentoBancario> ObterTodosComTipoLancamento()
        {
            return _context.LancamentoBancario.Include(x => x.TipoLancamento).AsNoTracking().ToList();
        }

    }
}
