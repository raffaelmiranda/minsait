using Microsoft.EntityFrameworkCore;
using SalesManagement.CashFlow.Domain.Entities;
using SalesManagement.CashFlow.Infrastructure.Extensions;
using SalesManagement.CashFlow.Infrastructure.Persistence.Configurations;

namespace SalesManagement.CashFlow.Infrastructure.Persistence.Contexts
{
    public class CashFlowContext : DbContext
    {
        public CashFlowContext(DbContextOptions<CashFlowContext> dbContextOptions) : base(dbContextOptions)
        {
            if (dbContextOptions is null)
            { throw new ArgumentNullException(nameof(dbContextOptions)); }
        }

        public DbSet<LancamentoBancario> LancamentoBancario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CashFlow");
            modelBuilder.ApplyConfiguration(new LancamentoBancarioConfiguration());
            modelBuilder.Seed();
        }
    }
}
