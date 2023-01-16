using Microsoft.EntityFrameworkCore;
using SalesManagement.Report.Domain.Entities;
using SalesManagement.Report.Domain.Interfaces.Repositories;
using SalesManagement.Report.Infrastructure.Persistence.Contexts;

namespace SalesManagement.Report.Infrastructure.Persistence.Repositories
{
    public class LancamentoBancarioRepository : BaseRepository<LancamentoBancario>, ILancamentoBancarioRepository
    {
        public LancamentoBancarioRepository(WorkerContext context) : base(context) { }

        public List<LancamentoBancario> ObterTodosComTipoLancamento()
        {
            return _context.LancamentoBancario.Include(x => x.TipoLancamento).AsNoTracking().ToList();
        }

    }
}
