using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading;
using UserService.Application.Features.Dtos;
using UserService.Application.Features.Login;
using UserService.Application.Features.Register;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Authentication;
using Xunit;
namespace UserService.Tests
{
    public class AuthTests
    {
        [Fact]
        public async Task RegisterUser_ShouldReturnJwt_WhenUserNotExists()
        {
            // Arrange
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByNameAsync("Andrey", It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);
            repoMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var passwordHasherMock = new Mock<IPasswordHasher>();
            passwordHasherMock.Setup(p => p.Hash("123456")).Returns("hashed-password");

            var jwtGeneratorMock = new Mock<IJwtTokenGenerator>();
            jwtGeneratorMock
                .Setup(j => j.GenerateToken(It.IsAny<User>()))
                .Returns(new AuthResponseDto(
                    AccessToken: "fake-jwt-token",
                    ExpiresAt: DateTime.UtcNow.AddHours(1)
                ));

            var handler = new RegisterUserHandler(
                repoMock.Object,
                passwordHasherMock.Object,
                jwtGeneratorMock.Object
            );

            // Act
            var response = await handler.Handle(new RegisterUserCommand("Andrey", "123456"), It.IsAny<CancellationToken>());

            // Assert
            response.AccessToken.Should().Be("fake-jwt-token");
            response.ExpiresAt.Should().BeAfter(DateTime.UtcNow);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnJwt_WhenCredentialsValid()
        {
            // Arrange
            var user = new User("Andrey", "123") { Id = 1 };

            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByNameAsync("Andrey", It.IsAny<CancellationToken>())).ReturnsAsync(user);

            var passwordHasherMock = new Mock<IPasswordHasher>();
            passwordHasherMock
                .Setup(p => p.Verify(user.Password, "123"))
                .Returns(true);

            var jwtGeneratorMock = new Mock<IJwtTokenGenerator>();
            jwtGeneratorMock
                .Setup(j => j.GenerateToken(It.IsAny<User>()))
                .Returns(new AuthResponseDto(
                    AccessToken: "fake-jwt-token",
                    ExpiresAt: DateTime.UtcNow.AddHours(1)
                ));

            var handler = new LoginUserHandler(
                repoMock.Object,
                passwordHasherMock.Object,
                jwtGeneratorMock.Object
            );

            var command = new LoginUserCommand("Andrey", "123");

            // Act
            var response = await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            response.AccessToken.Should().Be("fake-jwt-token");
            response.ExpiresAt.Should().BeAfter(DateTime.UtcNow);
        }
    }
}