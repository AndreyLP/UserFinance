using FinanceService.Application.Features.GetUserCurrencies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.API.Controllers
{
    [ApiController]
    [Route("finance")]
    [Authorize]
    public class FinanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("my-currencies")]
        public async Task<IActionResult> GetMyCurrencies(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetMyCurrenciesQuery(),
                cancellationToken
            );

            return Ok(result);
        }
    }
}
