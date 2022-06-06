namespace FBT.WebAPI.Features.RecurringExpenses
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Shared;
    using System.Collections.Generic;

    [Authorize]
    public class RecurringExpensesController : ApiController
    {
        private readonly IRecurringExpenseService recurringExpense;

        public RecurringExpensesController(IRecurringExpenseService recurringExpense)
        {
            this.recurringExpense = recurringExpense;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseResponseModel>> GetAll()
        {
            return await recurringExpense.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateExpenseModel model)
        {
            var id = await recurringExpense.Create(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateExpenseModel model)
        {
            var updated = await this.recurringExpense.Update(model);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
