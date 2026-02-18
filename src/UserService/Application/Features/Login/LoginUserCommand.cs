using MediatR;
using UserService.Application.Features.Dtos;

namespace UserService.Application.Features.Login
{
    public record LoginUserCommand(string Name, string Password) : IRequest<AuthResponseDto>;
}
