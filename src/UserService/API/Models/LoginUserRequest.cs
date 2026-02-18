namespace UserService.API.Models
{
    public record LoginUserRequest(
        string Name,
        string Password
    );
}
