using Microsoft.EntityFrameworkCore;
using SalesManagement.CashFlow.Domain.Entities;

namespace SalesManagement.CashFlow.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            TipoLancamento debit = new TipoLancamento(1, "Debit");
            modelBuilder.Entity<TipoLancamento>().HasData(debit);

            TipoLancamento credit = new TipoLancamento(2, "Credit");
            modelBuilder.Entity<TipoLancamento>().HasData(credit);

            LancamentoBancario lancamento01 = new LancamentoBancario(id: 1, descricao: "descrição 01", valor: 100.0M, tipoLancamentoId: debit.Id, categoria: "categoria 01", criadoEm: new DateTime(2023,1,12));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento01);

            LancamentoBancario lancamento02 = new LancamentoBancario(id: 2 , descricao: "descrição 02", valor: 100000.0M, tipoLancamentoId: debit.Id, categoria: "categoria 02", criadoEm: new DateTime(2023, 1, 12));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento02);

            LancamentoBancario lancamento03 = new LancamentoBancario(id: 3, descricao: "descrição 03", valor: 200000.0M, tipoLancamentoId: credit.Id, categoria: "categoria 03", criadoEm: new DateTime(2023, 1, 13));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento03);

            LancamentoBancario lancamento04 = new LancamentoBancario(id: 4, descricao: "descrição 04", valor: 100000.0M, tipoLancamentoId: credit.Id, categoria: "categoria 04", criadoEm: new DateTime(2023, 1, 13));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento04);

            LancamentoBancario lancamento05 = new LancamentoBancario(id: 5, descricao: "descrição 05", valor: 100.0M, tipoLancamentoId: debit.Id, categoria: "categoria 05", criadoEm: new DateTime(2023, 1, 14));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento05);

            LancamentoBancario lancamento06 = new LancamentoBancario(id: 6, descricao: "descrição 06", valor: 100000.0M, tipoLancamentoId: debit.Id, categoria: "categoria 06", criadoEm: new DateTime(2023, 1, 14));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento06);

            LancamentoBancario lancamento07 = new LancamentoBancario(id: 7, descricao: "descrição 07", valor: 200000.0M, tipoLancamentoId: credit.Id, categoria: "categoria 07", criadoEm: new DateTime(2023, 1, 15));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento07);

            LancamentoBancario lancamento08 = new LancamentoBancario(id: 8, descricao: "descrição 08", valor: 100000.0M, tipoLancamentoId: credit.Id, categoria: "categoria 08", criadoEm: new DateTime(2023, 1, 15));
            modelBuilder.Entity<LancamentoBancario>().HasData(lancamento08);
        }
    }
}

