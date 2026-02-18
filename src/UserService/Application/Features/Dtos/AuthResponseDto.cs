namespace UserService.Application.Features.Dtos
{
    public record AuthResponseDto(
        string AccessToken,
        DateTime ExpiresAt
    );
}
