namespace Infrastructure.Clients
{
    public class CbrClient : ICbrClient
    {
        private readonly HttpClient _httpClient;

        public CbrClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDailyRatesAsync(CancellationToken cancellationToken)
        {
            return await _httpClient.GetStringAsync("", cancellationToken);
        }
    }
}
