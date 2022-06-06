namespace FBT.WebAPI.Features.RecurringIncomes
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Shared;
    using System.Collections.Generic;

    [Authorize]
    public class RecurringIncomesController : ApiController
    {
        private readonly IRecurringIncomeService recurringIncome;

        public RecurringIncomesController(IRecurringIncomeService recurringIncome)
        {
            this.recurringIncome = recurringIncome;
        }

        [HttpGet]
        public async Task<IEnumerable<IncomeResponseModel>> GetAll()
        {
            return await recurringIncome.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateIncomeModel model)
        {
            var id = await recurringIncome.Create(model);

            return Created(nameof(this.Create), id);
        }


        [HttpPut]
        public async Task<ActionResult> Update(UpdateIncomeModel model)
        {
            var updated = await this.recurringIncome.Update(model);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
