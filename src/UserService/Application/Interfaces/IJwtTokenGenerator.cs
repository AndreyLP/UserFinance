using UserService.Application.Features.Dtos;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        AuthResponseDto GenerateToken(User user);
    }
}
