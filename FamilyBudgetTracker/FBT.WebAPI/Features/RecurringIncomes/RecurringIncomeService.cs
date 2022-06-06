namespace FBT.WebAPI.Features.RecurringIncomes
{
    using AutoMapper;
    using FBT.WebAPI.Data;
    using FBT.WebAPI.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Shared;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class RecurringIncomeService : IRecurringIncomeService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        private readonly IMapper mapper;
        private readonly FamilyBudgetTrackerDbContext dbContext;

        public RecurringIncomeService(IHttpContextAccessor httpContextAccessor, IMapper mapper, FamilyBudgetTrackerDbContext dbContext)
        {
            this.user = httpContextAccessor.HttpContext?.User;
            this.mapper = mapper;
            this.dbContext = dbContext;
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
            recurringIncome.TotalAmount = recurringIncome.Amount;

            this.dbContext.Add(recurringIncome);
            await this.dbContext.SaveChangesAsync();

            return recurringIncome.Id;
        }

        public async Task<bool> Update(IncomeModel model)
        {
            var recurringIncome = await this.dbContext.RecurringIncomes.FindAsync(model.Id);

            if (recurringIncome != null)
            {
                mapper.Map(model, recurringIncome);
                await this.dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
