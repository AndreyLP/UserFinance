using FinanceService.Application.Interfaces;
using FinanceService.Infrastructure.Identity;
using FinanceService.Infrastructure.Persistence;
using FinanceService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<FinanceDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("FinanceDb"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(FinanceDbContext).Assembly.FullName);
                        b.MigrationsHistoryTable("__EFMigrationsHistory", FinanceDbContext.Schema);
                    }));

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }
    }
}
