using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MigrationService.Services
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddPostgresContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionName,
            string schema)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString(connectionName),
                    dbOptions =>
                    {
                        dbOptions.MigrationsAssembly(typeof(TContext).Assembly.FullName);
                        dbOptions.MigrationsHistoryTable("__EFMigrationsHistory", schema);
                    }));

            return services;
        }
    }
}
