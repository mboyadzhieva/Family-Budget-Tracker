﻿namespace FBT.WebAPI.Features.RecurringExpenses
{
    using AutoMapper;
    using FBT.WebAPI.Data;
    using FBT.WebAPI.Data.Models;
    using Microsoft.AspNetCore.Http;
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
        public async Task<int> Create(RecurringExpenseRequestModel model)
        {
            string userId = user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var recurringExpense = mapper.Map<RecurringExpense>(model);
            recurringExpense.UserId = userId;
            recurringExpense.TotalAmount = 0M;

            this.dbContext.Add(recurringExpense);
            await this.dbContext.SaveChangesAsync();

            return recurringExpense.Id;
        }
    }
}