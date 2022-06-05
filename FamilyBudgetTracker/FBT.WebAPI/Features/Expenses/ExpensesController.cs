namespace FBT.WebAPI.Features.Expenses
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class ExpensesController : ApiController
    {
        private readonly IExpenseService expense;

        public ExpensesController(IExpenseService expense)
        {
            this.expense = expense;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ExpenseRequestModel model)
        {
            var id = await expense.Create(model);

            return Created(nameof(this.Create), id);
        }
    }
}
