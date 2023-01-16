namespace SalesManagement.Report.Domain.Entities
{
    public class Relatorio
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; } = string.Empty;
        public string Caminho { get; set; } = string.Empty;
    }
}
