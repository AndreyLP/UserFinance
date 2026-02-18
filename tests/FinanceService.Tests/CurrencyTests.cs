using FinanceService.Application.Features.GetUserCurrencies;
using FinanceService.Application.Interfaces;
using FinanceService.Domain.Entities;
using FluentAssertions;
using Moq;

namespace FinanceService.Tests
{
    public class CurrencyTests
    {
        [Fact]
        public async Task GetUserFavorites_ShouldReturnCurrencies()
        {
            // Arrange
            var usd = new Currency { Id = 1, Name = "USD" };
            usd.UpdateRate(80);

            var eur = new Currency { Id = 2, Name = "EUR" };
            eur.UpdateRate(90);

            var currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock
                .Setup(r => r.GetUserFavoritesCurrencyAsync(
                    42,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Currency> { usd, eur });

            var currentUserMock = new Mock<ICurrentUser>();
            currentUserMock
                .Setup(c => c.UserId)
                .Returns(42);

            var handler = new GetMyCurrenciesQueryHandler(
                currencyRepoMock.Object,
                currentUserMock.Object);

            var query = new GetMyCurrenciesQuery();

            // Act
            var result = await handler.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            result.Should().HaveCount(2);

            result[0].Name.Should().Be("USD");
            result[0].Rate.Should().Be(80);

            result[1].Name.Should().Be("EUR");
            result[1].Rate.Should().Be(90);
        }
    }
}