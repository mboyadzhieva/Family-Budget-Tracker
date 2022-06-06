namespace FBT.WebAPI.Features.Incomes
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Shared;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class IncomeService : IIncomeService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        private readonly IMapper mapper;
        private readonly FamilyBudgetTrackerDbContext dbContext;

        public IncomeService(IHttpContextAccessor httpContextAccessor, IMapper mapper, FamilyBudgetTrackerDbContext dbContext)
        {
            this.user = httpContextAccessor.HttpContext?.User;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IncomeResponseModel>> GetAll()
        {
            var userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            return await this.dbContext
                .Incomes
                .Where(i => i.UserId == userId)
                .Select(i => mapper.Map<IncomeResponseModel>(i))
                .ToListAsync();
        }

        public async Task<int> Create(CreateIncomeModel model)
        {
            string userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var income = mapper.Map<Income>(model);
            income.UserId = userId;

            this.dbContext.Add(income);
            await this.dbContext.SaveChangesAsync();

            return income.Id;
        }

        public async Task<bool> Update(UpdateIncomeModel model)
        {
            var income = await this.dbContext.Incomes.FindAsync(model.Id);

            if (income != null)
            {
                mapper.Map(model, income);
                await this.dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
