namespace FBT.WebAPI.Features.Expenses
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

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

        public async Task<IEnumerable<ExpenseModel>> GetAll()
        {
            var userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            return await this.dbContext
                .Expenses
                .Where(e => e.UserId == userId)
                .Select(e => mapper.Map<ExpenseModel>(e))
                .OrderBy(e => e.PaymentDate)
                .ToListAsync();
        }

        public async Task<ExpenseModel> Get(int id)
        {
            var expense = await this.dbContext.Expenses.FindAsync(id);

            return mapper.Map<ExpenseModel>(expense);
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

        public async Task<bool> Update(ExpenseModel model)
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

        public async Task<bool> Delete(int id)
        {
            var expense = await this.dbContext
                .Expenses
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (expense != null)
            {
                this.dbContext.Remove(expense);
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
