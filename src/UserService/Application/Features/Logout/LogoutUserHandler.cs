using MediatR;

namespace UserService.Application.Features.Logout
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand>
    {
        public Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
