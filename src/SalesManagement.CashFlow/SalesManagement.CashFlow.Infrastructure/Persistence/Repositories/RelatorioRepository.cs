using SalesManagement.CashFlow.Domain.Entities;
using SalesManagement.CashFlow.Domain.Interfaces.Repositories;
using SalesManagement.CashFlow.Infrastructure.Persistence.Contexts;

namespace SalesManagement.CashFlow.Infrastructure.Persistence.Repositories
{
    public class RelatorioRepository : BaseRepository<Relatorio>, IRelatorioRepository
    {
        public RelatorioRepository(CashFlowContext context) : base(context) { }
    }
}
