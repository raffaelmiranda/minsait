using SalesManagement.Report.Application.Feature;
using SalesManagement.Report.Application.Feature.Interfaces;
using SalesManagement.Report.Domain.Interfaces.Repositories;
using SalesManagement.Report.Infrastructure.Persistence.Repositories;

namespace SalesManagement.Report.Api.Configuration
{
    /// <summary>
    /// Dependency Injection nRegistration
    /// </summary>
    public static class DependencyInjectionRegistration
    {
        /// <summary>
        /// Registra as injeções de dependencias
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRelatorio, RelatorioApp>();
            services.AddTransient<IRelatorioRepository, RelatorioRepository>();
            services.AddTransient<ILancamentoBancarioRepository, LancamentoBancarioRepository>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
