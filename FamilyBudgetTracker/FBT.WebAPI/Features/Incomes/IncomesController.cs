namespace FBT.WebAPI.Features.Incomes
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Shared;
    using System.Collections.Generic;

    [Authorize]
    public class IncomesController : ApiController
    {
        private readonly IIncomeService income;

        public IncomesController(IIncomeService income)
        {
            this.income = income;
        }

        [HttpGet]
        public async Task<IEnumerable<IncomeResponseModel>> GetAll()
        {
            return await income.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateIncomeModel model)
        {
            var id = await income.Create(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateIncomeModel model)
        {
            var updated = await this.income.Update(model);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
