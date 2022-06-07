namespace FBT.WebAPI.Features.Budget
{
    using AutoMapper;
    using Data;
    using Shared;
    using System;
    using System.Linq;

    public class BudgetService : IBudgetService
    {
        private readonly FamilyBudgetTrackerDbContext dbContext;
        private readonly IMapper mapper;

        public BudgetService(
            FamilyBudgetTrackerDbContext dbContext,
            IMapper mapper
            )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public decimal Calculate(string userId)
        {
            var budget = 0M;

            budget += this.CalculateOneTimeIncomes(userId);

            budget += this.CalculateRecurringIncomes(userId);

            budget += this.CalculateOneTimeExpenses(userId);

            budget += this.CalculateRecurringExpenses(userId);

            return budget;
        }

        private decimal CalculateOneTimeIncomes(string userId)
        {
            var result = 0M;

            this.dbContext
                .Incomes
                .Where(i => i.UserId == userId && i.PaymentDate <= DateTime.UtcNow)
                .Select(i => mapper.Map<IncomeModel>(i))
                .ToList()
                .ForEach(i =>
                {
                    result += i.Amount;
                });

            return result;
        }

        private decimal CalculateOneTimeExpenses(string userId)
        {
            var result = 0M;

            this.dbContext
                .Expenses
                .Where(e => e.UserId == userId && e.PaymentDate <= DateTime.UtcNow)
                .Select(e => mapper.Map<ExpenseModel>(e))
                .ToList()
                .ForEach(e =>
                {
                    result -= e.Amount;
                });

            return result;
        }

        private decimal CalculateRecurringIncomes(string userId)
        {
            var result = 0M;

            this.dbContext
                .RecurringIncomes
                .Where(ri => ri.UserId == userId && ri.PaymentDate.Date <= DateTime.UtcNow.Date)
                .ToList()
                .ForEach(ri =>
                {
                    var currentDate = DateTime.UtcNow.Date;
                    var paymentDate = ri.PaymentDate.Date;

                    var passedMonths = Math.Abs((currentDate - paymentDate).Days) / 30;

                    ri.TotalAmount = ri.Amount * (passedMonths + 1);

                    this.dbContext.SaveChanges();
                    result += ri.TotalAmount;
                });

            return result;
        }

        private decimal CalculateRecurringExpenses(string userId)
        {
            var result = 0M;

            this.dbContext
                .RecurringExpenses
                .Where(re => re.UserId == userId && re.PaymentDate.Date <= DateTime.UtcNow.Date)
                .ToList()
                .ForEach(re =>
                {
                    var currentDate = DateTime.UtcNow.Date; // 7-june-2022 
                    var paymentDate = re.PaymentDate.Date; // 5-may-2022 // 8-may-2022

                    var passedMonths = Math.Abs((currentDate - paymentDate).Days) / 30; // 33 days == 1 month

                    re.TotalAmount = re.Amount * (passedMonths + 1);

                    this.dbContext.SaveChanges();
                    result -= re.TotalAmount;
                });

            return result;
        }
    }
}
