using SalesManagement.Report.Api.Configuration;
using SalesManagement.Report.Api.Configuration.Swagger;
using SalesManagement.Report.Infrastructure.Persistence.Contexts;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Register(builder.Configuration);
builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation(builder.Configuration, "SalesManagement.Report");
builder.Services.AddSwaggerGen((opts) => { opts.ExampleFilters(); });
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.RegisterEF<WorkerContext>(builder.Configuration, "SalesManagement");

var app = builder.Build();
app.UseRouting();
app.UseEndpoints((endpoints) => endpoints.MapControllers());

if (!app.Environment.IsProduction())
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

await app.RunAsync();
