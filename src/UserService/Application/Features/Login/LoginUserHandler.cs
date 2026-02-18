using MediatR;
using UserService.Application.Features.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Login
{
    public class LoginUserHandler
        : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public LoginUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponseDto> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByNameAsync(request.Name, cancellationToken);

            if (user == null)
                throw new Exception("Invalid credentials");

            var isValid = _passwordHasher.Verify(request.Password, user.Password);

            if (!isValid)
                throw new Exception("Invalid credentials");

            return _jwtGenerator.GenerateToken(user);
        }
    }
}
