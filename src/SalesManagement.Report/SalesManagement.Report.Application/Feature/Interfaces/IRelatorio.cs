namespace SalesManagement.Report.Application.Feature.Interfaces
{
    public interface IRelatorio
    {
        void Processar();
        byte[] Download(string nomeArquivo);

    }
}
