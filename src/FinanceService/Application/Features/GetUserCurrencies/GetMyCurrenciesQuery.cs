using FinanceService.Application.Dtos;
using MediatR;

namespace FinanceService.Application.Features.GetUserCurrencies
{
    public record GetMyCurrenciesQuery() : IRequest<IReadOnlyList<FavoriteCurrencyDto>>;
}
