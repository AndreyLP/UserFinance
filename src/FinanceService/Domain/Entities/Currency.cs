namespace FinanceService.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Rate { get; private set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public void UpdateRate(decimal? rate)
        {
            if (rate <= 0 && rate.HasValue)
                throw new ArgumentException("Rate must be positive", nameof(rate));

            Rate = rate;
        }
    }
}
