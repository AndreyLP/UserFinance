using FinanceService.Application.Dtos;
using FinanceService.Application.Interfaces;
using MediatR;

namespace FinanceService.Application.Features.GetUserCurrencies
{
    public sealed class GetMyCurrenciesQueryHandler
    : IRequestHandler<GetMyCurrenciesQuery, IReadOnlyList<FavoriteCurrencyDto>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrentUser _currentUser;

        public GetMyCurrenciesQueryHandler(
            ICurrencyRepository currencyRepository,
            ICurrentUser currentUser)
        {
            _currencyRepository = currencyRepository;
            _currentUser = currentUser;
        }

        public async Task<IReadOnlyList<FavoriteCurrencyDto>> Handle(
            GetMyCurrenciesQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var currencies = await _currencyRepository
                .GetUserFavoritesCurrencyAsync(userId, cancellationToken);

            return currencies
                .Select(x => new FavoriteCurrencyDto
                {
                    Name = x.Name,
                    Rate = x.Rate
                })
                .ToList();
        }
    }
}
