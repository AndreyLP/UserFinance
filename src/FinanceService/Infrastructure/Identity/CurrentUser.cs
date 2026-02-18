using FinanceService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceService.Infrastructure.Identity
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userId = _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst("sub")?.Value;

                return int.Parse(userId!);
            }
        }
    }
}
