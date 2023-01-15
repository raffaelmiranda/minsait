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

            LancamentoBancario statement01 = new LancamentoBancario(id: 1, descricao: "Conta da Sabesp", valor: 100.0M, tipoLancamentoId: debit.Id, categoria: "Agua");
            modelBuilder.Entity<LancamentoBancario>().HasData(statement01);

            LancamentoBancario statement02 = new LancamentoBancario(id: 2 , descricao: "Despesa com funcionários", valor: 100000.0M, tipoLancamentoId: debit.Id, categoria: "Folha de Pagamento");
            modelBuilder.Entity<LancamentoBancario>().HasData(statement02);

            LancamentoBancario statement03 = new LancamentoBancario(id: 3, descricao: "Venda para client X", valor: 200000.0M, tipoLancamentoId: credit.Id, categoria: "Recebivéis");
            modelBuilder.Entity<LancamentoBancario>().HasData(statement03);

            LancamentoBancario statement04 = new LancamentoBancario(id: 4, descricao: "Venda para client Y", valor: 100000.0M, tipoLancamentoId: credit.Id, categoria: "Recebivéis");
            modelBuilder.Entity<LancamentoBancario>().HasData(statement04);
        }
    }
}

