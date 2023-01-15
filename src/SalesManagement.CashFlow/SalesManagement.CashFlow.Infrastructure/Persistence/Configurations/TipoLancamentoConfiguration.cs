using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.CashFlow.Domain.Entities;
namespace SalesManagement.CashFlow.Infrastructure.Persistence.Configurations
{
    public class TipoLancamentoConfiguration : IEntityTypeConfiguration<TipoLancamento>
    {
        public void Configure(EntityTypeBuilder<TipoLancamento> builder)
        {
            builder.ToTable(nameof(TipoLancamento))
            .HasKey(x => x.Id);

            builder.Property(c => c.Id);

            builder.Property(x => x.CriadoEm)
                .HasColumnType("datetime")
                .HasColumnName("CriadoEm")
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(100)")
                .HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
