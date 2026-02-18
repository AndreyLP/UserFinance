namespace Infrastructure.Clients
{
    public interface ICbrClient
    {
        Task<string> GetDailyRatesAsync(CancellationToken cancellationToken);
    }
}
