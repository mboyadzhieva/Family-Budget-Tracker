namespace FBT.WebAPI.Features.Incomes
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class IncomesController : ApiController
    {
        private readonly IIncomeService income;

        public IncomesController(IIncomeService income)
        {
            this.income = income;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(IncomeRequestModel model)
        {
            var id = await income.Create(model);

            return Created(nameof(this.Create), id);
        }
    }
}
