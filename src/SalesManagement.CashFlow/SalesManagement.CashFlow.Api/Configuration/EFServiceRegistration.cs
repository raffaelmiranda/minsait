using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManagement.CashFlow.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.CashFlow.Infrastructure.Extensions
{
    public static class EFServiceRegistration
    {
        public static IServiceCollection RegisterEF<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string schema = "dbo")
            where T : DbContext
        {
            services.AddDbContextPool<T>(
                (serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString(
                        typeof(T).Name),
                        option =>
                        {
                            option.MigrationsHistoryTable("__EFMigrationsHistory", schema);
                            option.EnableRetryOnFailure();
                            option.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                },100);

            return services;
        }

        public static async Task MigrateDatabaseAsync<T>(this IServiceProvider servicesProvider, int minutesTimeout = 5)
            where T : DbContext
        {
            using var scope = servicesProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<T>();

            context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(minutesTimeout).TotalMilliseconds);
            await context.Database.MigrateAsync();
        }
    }
}
