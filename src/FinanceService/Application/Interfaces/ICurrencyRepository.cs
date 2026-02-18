using FinanceService.Domain.Entities;

namespace FinanceService.Application.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetUserFavoritesCurrencyAsync(int user_id, CancellationToken cancellationToken);
    }
}
