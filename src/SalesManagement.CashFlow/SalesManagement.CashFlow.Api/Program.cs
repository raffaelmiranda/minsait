using SalesManagement.CashFlow.Api.Configuration;
using SalesManagement.CashFlow.Api.Configuration.Swagger;
using SalesManagement.CashFlow.Infrastructure.Extensions;
using SalesManagement.CashFlow.Infrastructure.Persistence.Contexts;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Register(builder.Configuration);
builder.Services.RegisterEF<CashFlowContext>(builder.Configuration, "SalesManagement");
builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation(builder.Configuration, "SalesManagement.CashFlow");
builder.Services.AddSwaggerGen((opts) => { opts.ExampleFilters(); });
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

var app = builder.Build();
app.UseRouting();
app.UseEndpoints((endpoints) => endpoints.MapControllers());

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "version 1");
        s.InjectJavascript("/swagger-ui/custom.js");
        s.InjectStylesheet("/swagger-ui/custom.css");
    });
    //app.UseSwaggerAuthorized();
    app.UseSwaggerDocumentation();
}

await app.Services.MigrateDatabaseAsync<CashFlowContext>();
await app.RunAsync();
