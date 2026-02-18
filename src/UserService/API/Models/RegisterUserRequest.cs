namespace UserService.API.Models
{
    public record RegisterUserRequest(
        string Name,
        string Password
    );
}
