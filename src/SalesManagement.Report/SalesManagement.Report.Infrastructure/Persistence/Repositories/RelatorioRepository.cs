using SalesManagement.Report.Domain.Entities;
using SalesManagement.Report.Domain.Interfaces.Repositories;
using SalesManagement.Report.Infrastructure.Persistence.Contexts;

namespace SalesManagement.Report.Infrastructure.Persistence.Repositories
{
    public class RelatorioRepository : BaseRepository<Relatorio>, IRelatorioRepository
    {
        public RelatorioRepository(WorkerContext context) : base(context) { }
    }
}
