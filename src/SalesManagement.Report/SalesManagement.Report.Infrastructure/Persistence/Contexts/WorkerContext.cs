using Microsoft.EntityFrameworkCore;
using SalesManagement.Report.Domain.Entities;
using SalesManagement.Report.Infrastructure.Persistence.Configurations;

namespace SalesManagement.Report.Infrastructure.Persistence.Contexts
{
    public class WorkerContext : DbContext
    {
        public WorkerContext(DbContextOptions<WorkerContext> dbContextOptions) : base(dbContextOptions)
        {
            if (dbContextOptions is null)
            { throw new ArgumentNullException(nameof(dbContextOptions)); }
        }

        public DbSet<LancamentoBancario> LancamentoBancario { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CashFlow");
            modelBuilder.ApplyConfiguration(new LancamentoBancarioConfiguration());
            modelBuilder.ApplyConfiguration(new RelatorioConfiguration());
            
        }
    }
}
