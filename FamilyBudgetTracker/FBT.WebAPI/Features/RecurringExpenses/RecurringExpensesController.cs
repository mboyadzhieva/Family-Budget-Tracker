namespace FBT.WebAPI.Features.RecurringExpenses
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    public class RecurringExpensesController : ApiController
    {
        private readonly IRecurringExpenseService recurringExpense;

        public RecurringExpensesController(IRecurringExpenseService recurringExpense)
        {
            this.recurringExpense = recurringExpense;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseModel>> GetAll()
        {
            return await recurringExpense.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ExpenseModel> Get(int id)
        {
            return await recurringExpense.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateExpenseModel model)
        {
            var id = await recurringExpense.Create(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(ExpenseModel model)
        {
            var updated = await this.recurringExpense.Update(model);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await this.recurringExpense.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
