using Infrastructure.Clients;
using Infrastructure.Persistence;
using Infrastructure.Xml;
using Microsoft.EntityFrameworkCore;

namespace Worker
{
    public class CurrencyUpdateWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CurrencyUpdateWorker> _logger;
        private readonly ICbrClient _cbrClient;

        public CurrencyUpdateWorker(
            IServiceProvider serviceProvider,
            ILogger<CurrencyUpdateWorker> logger,
            ICbrClient cbrClient)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _cbrClient = cbrClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateCurrencies(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Currency update failed");
                }

                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }
        }

        private async Task UpdateCurrencies(CancellationToken cancellationToken)
        {
            var xml = await _cbrClient.GetDailyRatesAsync(cancellationToken);

            var parsed = CbrCurrencyParser.Parse(xml);

            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CurrencyUpdaterDbContext>();

            foreach (var currency in parsed)
            {
                var existing = await db.Currencies
                    .FirstOrDefaultAsync(c => c.Name == currency.Name, cancellationToken);

                if (existing != null)
                {
                    existing.Rate = currency.Rate;
                }
                else
                {
                    db.Currencies.Add(new Currency
                    {
                        Name = currency.Name,
                        Rate = currency.Rate
                    });
                }
            }

            await db.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Currencies updated at {time}", DateTime.UtcNow);
        }
    }
}
