namespace FBT.WebAPI.Features.RecurringIncomes
{
    using AutoMapper;
    using FBT.WebAPI.Data;
    using FBT.WebAPI.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

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
        public async Task<int> Create(RecurringIncomeRequestModel model)
        {
            string userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var recurringIncome = mapper.Map<RecurringIncome>(model);
            recurringIncome.UserId = userId;
            recurringIncome.TotalAmount = 0M;

            this.dbContext.Add(recurringIncome);
            await this.dbContext.SaveChangesAsync();

            return recurringIncome.Id;
        }
    }
}
