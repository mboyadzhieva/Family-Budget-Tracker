namespace FBT.WebAPI.Features.RecurringIncomes
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Features.Budget;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class RecurringIncomeService : IRecurringIncomeService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        private readonly IMapper mapper;
        private readonly FamilyBudgetTrackerDbContext dbContext;
        private readonly IBudgetService budget;

        public RecurringIncomeService
            (IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            FamilyBudgetTrackerDbContext dbContext,
            IBudgetService budget
            )
        {
            this.user = httpContextAccessor.HttpContext?.User;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.budget = budget;
        }

        public async Task<IEnumerable<IncomeModel>> GetAll()
        {
            var userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            return await this.dbContext
                .RecurringIncomes
                .Where(ri => ri.UserId == userId)
                .Select(ri => mapper.Map<IncomeModel>(ri))
                .ToListAsync();
        }

        public async Task<IncomeModel> Get(int id)
        {
            var income = await this.dbContext.RecurringIncomes.FindAsync(id);

            return mapper.Map<IncomeModel>(income);
        }

        public async Task<int> Create(CreateIncomeModel model)
        {
            string userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var recurringIncome = mapper.Map<RecurringIncome>(model);
            recurringIncome.UserId = userId;

            this.dbContext.Add(recurringIncome);
            await this.dbContext.SaveChangesAsync();

            budget.Calculate(userId);

            return recurringIncome.Id;
        }

        public async Task<bool> Update(IncomeModel model)
        {
            var recurringIncome = await this.dbContext.RecurringIncomes.FindAsync(model.Id);

            if (recurringIncome != null)
            {
                mapper.Map(model, recurringIncome);
                recurringIncome.TotalAmount = 0M;

                await this.dbContext.SaveChangesAsync();
                budget.Calculate(recurringIncome.UserId);

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var recurringIncomes = await this.dbContext
                .RecurringIncomes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (recurringIncomes != null)
            {
                this.dbContext.Remove(recurringIncomes);
                await this.dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
