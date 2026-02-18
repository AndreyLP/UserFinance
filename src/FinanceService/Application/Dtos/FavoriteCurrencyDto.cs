namespace FinanceService.Application.Dtos
{
    public class FavoriteCurrencyDto
    {
        public string Name { get; set; } = null!;
        public decimal? Rate { get; set; }
    }
}
