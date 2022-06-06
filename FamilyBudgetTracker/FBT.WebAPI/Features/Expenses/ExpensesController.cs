namespace FBT.WebAPI.Features.Expenses
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Shared;
    using System.Collections.Generic;

    [Authorize]
    public class ExpensesController : ApiController
    {
        private readonly IExpenseService expense;

        public ExpensesController(IExpenseService expense)
        {
            this.expense = expense;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseModel>> GetAll()
        {
            return await expense.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ExpenseModel> Get(int id)
        {
            return await expense.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateExpenseModel model)
        {
            var id = await expense.Create(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(ExpenseModel model)
        {
            var updated = await this.expense.Update(model);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
