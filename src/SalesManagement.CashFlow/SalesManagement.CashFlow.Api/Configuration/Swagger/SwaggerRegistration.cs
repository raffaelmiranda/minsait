using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SalesManagement.CashFlow.Api.Configuration.Swagger
{
    public static class SwaggerRegistration
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration, string path, int version = 1)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(version, 0);
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });


            services.Configure<SwaggerAuthorizedConfig>(configuration.GetSection("SwaggerAuthorizedConfig"));

            services.AddSwaggerGen(cfg =>
            {
                cfg.DocumentFilter<SwaggerEnumDescriptions>();
                //cfg.OperationFilter<SwaggerHeaderAttribute>();
                cfg.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");

                var xmlFile = $"{Assembly.Load(path + ".Api").GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);

                //cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Description = "Concatene o Token JWT com a palavra Bearer(Bearer + Token)",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey
                //});
                //cfg.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //{
                //    new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference
                //        {
                //            Type = ReferenceType.SecurityScheme,
                //            Id = "Bearer"
                //        }
                //    },
                //    new string[] { }
                //}});
                cfg.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwaggerAuthorized();
            app.UseSwagger(opt =>
            {
                opt.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers.Clear();
                });
            });
            app.UseSwaggerUI(cfg =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    cfg.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
            });
            return app;
        }

        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
            => builder.UseMiddleware<SwaggerAuthorization>();
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "SalesManagement - API",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact
                {
                    Name = "Equipe Carrefour",
                },
            };

            if (description.IsDeprecated)
                info.Description += "Esta API esta depreciada.";

            return info;
        }
    }
}
