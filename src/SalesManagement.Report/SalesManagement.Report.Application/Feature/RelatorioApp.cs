using CsvHelper;
using CsvHelper.Configuration;
using SalesManagement.Report.Application.Feature.Interfaces;
using SalesManagement.Report.Domain.Entities;
using SalesManagement.Report.Domain.Interfaces.Repositories;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SalesManagement.Report.Application.Feature
{
    public class RelatorioApp: IRelatorio
    {
        private readonly ILancamentoBancarioRepository _repoLancamento;
        private readonly IRelatorioRepository _repoRelatorio;


        public RelatorioApp(ILancamentoBancarioRepository repoLancamento, IRelatorioRepository repoRelatorio)
        {
            _repoLancamento = repoLancamento;
            _repoRelatorio = repoRelatorio;

        }

        public void Processar()
        {
            List<LancamentoBancario> lancamentos = _repoLancamento.ObterTodosComTipoLancamento();

            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
            string fullPathFile = string.Empty;
            string nomeArquivo = $"{DateTime.Now:yyyyMMdd}-relatorio-consolidado.csv";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) fullPathFile = $"{Directory.GetCurrentDirectory()}\\{nomeArquivo}";
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) fullPathFile = $"{Directory.GetCurrentDirectory()}/{nomeArquivo}";
            
                

            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(fullPathFile))
            using (var csvWriter = new CsvWriter(writer, config))
            {
                Relatorio relatorio = new Relatorio();
                relatorio.NomeArquivo = nomeArquivo;
                relatorio.Caminho = fullPathFile;

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
            string fullPathFile = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) fullPathFile = $"{Directory.GetCurrentDirectory()}\\{nomeArquivo}";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) fullPathFile = $"{Directory.GetCurrentDirectory()}/{nomeArquivo}";

            return File.ReadAllBytes(fullPathFile);
        }

        public List<Relatorio> Obter()
        {
            return _repoRelatorio.Obter();
        }
    }
}
