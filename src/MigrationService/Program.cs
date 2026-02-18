using FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MigrationService.Services;
using UserService.Infrastructure.Persistence;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();

services.AddLogging(builder => builder.AddConsole());

services
    .AddPostgresContext<UserDbContext>(configuration, "UserDb", UserDbContext.Schema)
    .AddPostgresContext<FinanceDbContext>(configuration, "FinanceDb", FinanceDbContext.Schema);

services.AddScoped<MigrationRunner>();

using var provider = services.BuildServiceProvider();
using var scope = provider.CreateScope();

var runner = scope.ServiceProvider.GetRequiredService<MigrationRunner>();

await runner.RunAsync();
