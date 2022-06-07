namespace FBT.WebAPI.Features.RecurringExpenses
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

    public class RecurringExpenseService : IRecurringExpenseService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        private readonly IMapper mapper;
        private readonly FamilyBudgetTrackerDbContext dbContext;

        public RecurringExpenseService(IHttpContextAccessor httpContextAccessor, IMapper mapper, FamilyBudgetTrackerDbContext dbContext)
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
                .RecurringExpenses
                .Where(re => re.UserId == userId)
                .Select(re => mapper.Map<ExpenseModel>(re))
                .OrderBy(re => re.PaymentDate)
                .ToListAsync();
        }

        public async Task<ExpenseModel> Get(int id)
        {
            var expense = await this.dbContext.RecurringExpenses.FindAsync(id);

            return mapper.Map<ExpenseModel>(expense);
        }

        public async Task<int> Create(CreateExpenseModel model)
        {
            string userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var recurringExpense = mapper.Map<RecurringExpense>(model);
            recurringExpense.UserId = userId;

            this.dbContext.Add(recurringExpense);
            await this.dbContext.SaveChangesAsync();

            return recurringExpense.Id;
        }

        public async Task<bool> Update(ExpenseModel model)
        {
            var recurringExpense = await this.dbContext.RecurringExpenses.FindAsync(model.Id);

            if (recurringExpense != null)
            {
                mapper.Map(model, recurringExpense);
                await this.dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var recurringExpense = await this.dbContext
                .RecurringExpenses
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (recurringExpense != null)
            {
                this.dbContext.Remove(recurringExpense);
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
