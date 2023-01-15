namespace SalesManagement.CashFlow.Api.Configuration.Swagger
{
    public class SwaggerAuthorizedConfig
    {
        public string Identity { get; set; }
        public string Secret { get; set; }
        public int DuracaoMinutosLogin { get; set; }
    }
}
