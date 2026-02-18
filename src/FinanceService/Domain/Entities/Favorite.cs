namespace FinanceService.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }

        public Currency? Currency { get; set; }
    }
}
