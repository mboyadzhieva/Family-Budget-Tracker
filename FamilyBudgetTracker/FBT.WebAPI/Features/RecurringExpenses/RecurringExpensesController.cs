namespace FBT.WebAPI.Features.RecurringExpenses
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class RecurringExpensesController : ApiController
    {
        private readonly IRecurringExpenseService recurringExpense;

        public RecurringExpensesController(IRecurringExpenseService recurringExpense)
        {
            this.recurringExpense = recurringExpense;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(RecurringExpenseRequestModel model)
        {
            var id = await recurringExpense.Create(model);

            return Created(nameof(this.Create), id);
        }
    }
}
