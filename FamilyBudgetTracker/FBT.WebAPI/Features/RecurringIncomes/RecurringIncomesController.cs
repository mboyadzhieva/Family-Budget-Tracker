namespace FBT.WebAPI.Features.RecurringIncomes
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class RecurringIncomesController : ApiController
    {
        private readonly IRecurringIncomeService recurringIncome;

        public RecurringIncomesController(IRecurringIncomeService recurringIncome)
        {
            this.recurringIncome = recurringIncome;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(RecurringIncomeRequestModel model)
        {
            var id = await recurringIncome.Create(model);

            return Created(nameof(this.Create), id);
        }
    }
}
