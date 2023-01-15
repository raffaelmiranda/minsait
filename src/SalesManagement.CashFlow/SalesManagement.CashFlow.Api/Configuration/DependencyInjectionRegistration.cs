using Microsoft.EntityFrameworkCore;
using SalesManagement.CashFlow.Application.AppServices;
using SalesManagement.CashFlow.Application.Interfaces;
using SalesManagement.CashFlow.Domain.Interfaces.Repositories;
using SalesManagement.CashFlow.Infrastructure.Persistence.Contexts;
using SalesManagement.CashFlow.Infrastructure.Persistence.Repositories;

namespace SalesManagement.CashFlow.Api.Configuration
{
    public static class DependencyInjectionRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddTransient<ILancamentoBancarioAppService, LancamentoBancarioAppService>();
            services.AddTransient(typeof(IBaseAppService<>), typeof(BaseAppService<>));

            services.AddTransient<ILancamentoBancarioRepository, LancamentoBancarioRepository>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));




        }
    }
}
