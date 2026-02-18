using FinanceService.Application.Interfaces;
using FinanceService.Domain.Entities;
using FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly FinanceDbContext _context;

        public CurrencyRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Currency>> GetUserFavoritesCurrencyAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.UserFavorites
                .Where(f => f.UserId == userId)
                .Select(f => f.Currency!)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
