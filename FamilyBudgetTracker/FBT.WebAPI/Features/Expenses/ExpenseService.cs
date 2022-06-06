namespace FBT.WebAPI.Features.Expenses
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

    public class ExpenseService : IExpenseService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        private readonly IMapper mapper;
        private readonly FamilyBudgetTrackerDbContext dbContext;

        public ExpenseService(IHttpContextAccessor httpContextAccessor, IMapper mapper, FamilyBudgetTrackerDbContext dbContext)
        {
            this.user = httpContextAccessor.HttpContext?.User;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ExpenseResponseModel>> GetAll()
        {
            var userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            return await this.dbContext
                .Expenses
                .Where(e => e.UserId == userId)
                .Select(e => mapper.Map<ExpenseResponseModel>(e))
                .ToListAsync();
        }

        public async Task<int> Create(CreateExpenseModel model)
        {
            string userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var expense = mapper.Map<Expense>(model);
            expense.UserId = userId;

            this.dbContext.Add(expense);
            await this.dbContext.SaveChangesAsync();

            return expense.Id;
        }

        public async Task<bool> Update(UpdateExpenseModel model)
        {
            var expense = await this.dbContext.Expenses.FindAsync(model.Id);

            if (expense != null)
            {
                mapper.Map(model, expense);
                await this.dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
