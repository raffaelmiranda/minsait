using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SalesManagement.CashFlow.Api.Configuration.Swagger
{
    public class SwaggerHeaderAttribute : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "ClientId",
                In = ParameterLocation.Header,
                Required = true,
            });

        }
    }
}
