using MediatR;
using UserService.Application.Features.Dtos;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public RegisterUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
                .GetByNameAsync(request.Name, cancellationToken);

            if (existingUser != null)
                throw new Exception("User already exists");

            var hashedPassword = _passwordHasher.Hash(request.Password);

            var user = new User(request.Name, hashedPassword);

            await _userRepository.AddAsync(user, cancellationToken);

            var token = _jwtGenerator.GenerateToken(user);

            return token;
        }
    }
}
