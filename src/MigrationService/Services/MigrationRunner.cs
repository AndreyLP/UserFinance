using FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserService.Infrastructure.Persistence;

namespace MigrationService.Services
{
    public sealed class MigrationRunner
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MigrationRunner> _logger;

        public MigrationRunner(
            IServiceProvider serviceProvider,
            ILogger<MigrationRunner> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting database migrations...");

            using var scope = _serviceProvider.CreateScope();

            var userDb = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            var financeDb = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();

            var contexts = new DbContext[] { userDb, financeDb };

            foreach (var context in contexts)
            {
                var contextName = context.GetType().Name;

                _logger.LogInformation("Applying migrations for {Context}...", contextName);

                var pendingMigrations =
                    await context.Database.GetPendingMigrationsAsync(cancellationToken);

                var migrations = pendingMigrations.ToList();

                if (migrations.Count == 0)
                {
                    _logger.LogInformation(
                        "No pending migrations for {Context}.",
                        contextName);
                    continue;
                }

                _logger.LogInformation(
                    "Found {Count} pending migrations for {Context}. Applying...",
                    migrations.Count,
                    contextName);

                await context.Database.MigrateAsync(cancellationToken);

                _logger.LogInformation(
                    "Migrations successfully applied for {Context}.",
                    contextName);
            }

            _logger.LogInformation("All migrations completed successfully.");
        }
    }
}
