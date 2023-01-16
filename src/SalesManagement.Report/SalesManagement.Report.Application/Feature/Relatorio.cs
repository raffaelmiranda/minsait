using CsvHelper;
using CsvHelper.Configuration;
using SalesManagement.Report.Application.Feature.Interfaces;
using SalesManagement.Report.Domain.Entities;
using SalesManagement.Report.Domain.Interfaces.Repositories;
using System.Globalization;
using System.IO;
using System.Text;

namespace SalesManagement.Report.Application.Feature
{
    public class Relatorio: IRelatorio
    {
        private readonly ILancamentoBancarioRepository _repoLancamento;
        private readonly IRelatorioRepository _repoRelatorio;

        private readonly string path;

        public Relatorio(ILancamentoBancarioRepository repoLancamento, IRelatorioRepository repoRelatorio)
        {
            _repoLancamento = repoLancamento;
            _repoRelatorio = repoRelatorio;

            path = $"{Directory.GetCurrentDirectory()}\\";
     
        }

        public void Processar()
        {
            List<LancamentoBancario> lancamentos = _repoLancamento.ObterTodosComTipoLancamento();

            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };

            if (!Directory.Exists($"{path}relatorio")) Directory.CreateDirectory($"{path} relatorio");

            string nomeArquivo = $"{DateTime.Now:yyyyMMdd}-relatorio-consolidado.csv";

            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter($"{path}relatorio\\{nomeArquivo}"))
            using (var csvWriter = new CsvWriter(writer, config))
            {
                Domain.Entities.Relatorio relatorio = new Domain.Entities.Relatorio();
                relatorio.NomeArquivo = nomeArquivo;
                relatorio.Caminho = $"{path}relatorio\\{nomeArquivo}";

                int index = 0;

                decimal valorPorDia = 0.0M;

                csvWriter.WriteField("Data");
                csvWriter.WriteField("Valor");
                csvWriter.NextRecord();

                foreach (var lancamentoGroup in lancamentos.GroupBy(x => new { x.CriadoEm }))
                {
                    csvWriter.WriteField(lancamentoGroup.Key.CriadoEm.ToShortDateString());

                    foreach (var item in lancamentoGroup)
                    {
                        if (item.TipoLancamento.Nome == "Debit") item.SetValor(item.Valor * -1);

                        valorPorDia += item.Valor;
                    }

                    csvWriter.WriteField(valorPorDia);

                    if (index < lancamentos.Count)
                        csvWriter.NextRecord();                    
                }

                writer.Flush();

                _repoRelatorio.Salvar(relatorio);
            }
        }

        public byte[] Download(string nomeArquivo)
        {
            return File.ReadAllBytes(($"{path}relatorio\\{nomeArquivo}.csv"));
        }
    }
}
