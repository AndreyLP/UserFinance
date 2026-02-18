using MediatR;
using UserService.Application.Features.Dtos;

namespace UserService.Application.Features.Register
{
    public record RegisterUserCommand(string Name, string Password)
        : IRequest<AuthResponseDto>;
}
