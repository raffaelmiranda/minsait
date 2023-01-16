using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Report.Domain.Entities;

namespace SalesManagement.Report.Infrastructure.Persistence.Configurations
{
    public class RelatorioConfiguration : IEntityTypeConfiguration<Relatorio>
    {
        public void Configure(EntityTypeBuilder<Relatorio> builder)
        {
            builder.ToTable(nameof(Relatorio))
             .HasKey(x => x.Id);

            builder.Property(x => x.NomeArquivo)
                .HasColumnType("varchar(500)")
                .HasColumnName("NomeArquivo")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(x => x.Caminho)
               .HasColumnType("varchar(500)")
               .HasColumnName("Caminho")
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode(false);
        }
    }
}
