namespace FBT.WebAPI.Features.Budget
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;

    [Authorize]
    public class BudgetController : ApiController
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        private readonly IBudgetService budget;

        public BudgetController(IBudgetService budget, IHttpContextAccessor httpContextAccessor)
        {
            this.budget = budget;
            this.user = httpContextAccessor.HttpContext?.User;
        }

        [HttpGet]
        public ActionResult<decimal> Calculate()
        {
            var userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            return budget.Calculate(userId);
        }
    }
}
