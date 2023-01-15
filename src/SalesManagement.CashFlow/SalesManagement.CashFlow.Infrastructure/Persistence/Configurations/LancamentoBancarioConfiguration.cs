using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.CashFlow.Domain.Entities;

namespace SalesManagement.CashFlow.Infrastructure.Persistence.Configurations
{
    public class LancamentoBancarioConfiguration : IEntityTypeConfiguration<LancamentoBancario>
    {
        public void Configure(EntityTypeBuilder<LancamentoBancario> builder)
        {
            builder.ToTable(nameof(LancamentoBancario))
              .HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.CriadoEm)
                .HasColumnType("datetime")
                .HasColumnName("CriadoEm")
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                .HasColumnType("varchar(500)")
                .HasColumnName("Descricao")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Valor")
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Categoria)
                .HasColumnType("varchar(100)")
                .HasColumnName("Categoria")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder
              .HasIndex(x => x.TipoLancamentoId)
              .HasDatabaseName("IX_LancamentoBancario_TipoLancamentoId").IsUnique(false);

            builder
                .HasOne(x => x.TipoLancamento)
                .WithOne(y => y.LancamentoBancario)
                .HasForeignKey<LancamentoBancario>(a => a.TipoLancamentoId);

        }
    }
}
