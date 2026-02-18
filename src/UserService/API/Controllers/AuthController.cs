using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Models;
using UserService.Application.Features.Login;
using UserService.Application.Features.Register;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                request.Name,
                request.Password);
            var token = await _mediator.Send(command, cancellationToken);
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(
                request.Name,
                request.Password);
            var token = await _mediator.Send(command, cancellationToken);
            return Ok(token);
        }
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
