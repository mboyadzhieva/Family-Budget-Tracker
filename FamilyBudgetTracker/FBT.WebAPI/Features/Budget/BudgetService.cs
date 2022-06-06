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

            this.dbContext
                .Incomes
                .Where(i => i.UserId == userId)
                .Select(i => mapper.Map<IncomeResponseModel>(i))
                .ToList()
                .ForEach(i =>
                {
                    budget += i.Amount;
                });

            this.dbContext
                .Expenses
                .Where(e => e.UserId == userId)
                .Select(e => mapper.Map<ExpenseResponseModel>(e))
                .ToList()
                .ForEach(e =>
                {
                    budget -= e.Amount;
                });

            var recurring = this.UpdateRecurringPayments(userId);
            budget += recurring;

            return budget;
        }

        private decimal UpdateRecurringPayments(string userId)
        {
            var recurringBudget = 0M;

            this.dbContext
                .RecurringIncomes
                .Where(ri => ri.UserId == userId)
                .ToList()
                .ForEach(ri =>
                {
                    if (ri.ModifiedOn != null)
                    {
                        var modifiedDate = (DateTime)ri.ModifiedOn;

                        if (modifiedDate.Date < DateTime.UtcNow.Date
                            && (modifiedDate.Month != DateTime.UtcNow.Month))
                        {
                            ri.TotalAmount += ri.Amount;
                            ri.ModifiedOn = ri.PaymentDate.AddMonths(1);

                            this.dbContext.SaveChanges();

                            recurringBudget += ri.TotalAmount;

                            //recurringBudget += (ri.TotalAmount + ri.Amount);
                        }
                        else
                        {
                            recurringBudget += ri.TotalAmount;
                        }
                    }
                    else
                    {
                        if (ri.PaymentDate.Date < DateTime.UtcNow.Date
                            && (ri.PaymentDate.Month != DateTime.UtcNow.Month))
                        {
                            ri.TotalAmount += ri.Amount;
                            ri.ModifiedOn = ri.PaymentDate.AddMonths(1);

                            this.dbContext.SaveChanges();

                            recurringBudget += ri.TotalAmount;

                            // recurringBudget += (ri.TotalAmount + ri.Amount);
                        }
                        else
                        {
                            recurringBudget += ri.Amount;
                        }
                    }
                });

            this.dbContext
                .RecurringExpenses
                .Where(re => re.UserId == userId)
                .ToList()
                .ForEach(re =>
                {
                    if (re.ModifiedOn != null)
                    {
                        var modifiedDate = (DateTime)re.ModifiedOn;

                        // check if total amount was updated for the current month's expenses
                        if (modifiedDate.Day < DateTime.UtcNow.Day
                            && (modifiedDate.Month != DateTime.UtcNow.Month))
                        {
                            re.TotalAmount += re.Amount;
                            re.ModifiedOn = re.PaymentDate.AddMonths(1);

                            this.dbContext.SaveChanges();

                            recurringBudget -= re.TotalAmount;

                            // recurringBudget -= (re.TotalAmount + re.Amount);
                        }
                        else
                        {
                            recurringBudget -= re.TotalAmount;
                        }
                    }
                    else
                    {
                        if (re.PaymentDate.Date < DateTime.UtcNow.Date
                            && (re.PaymentDate.Month != DateTime.UtcNow.Month))
                        {
                            re.TotalAmount += re.Amount;
                            re.ModifiedOn = re.PaymentDate.AddMonths(1);

                            this.dbContext.SaveChanges();

                            recurringBudget -= re.TotalAmount;

                            // recurringBudget -= (re.TotalAmount + re.Amount);
                        }
                        else
                        {
                            recurringBudget -= re.Amount;
                        }
                    }
                });

            return recurringBudget;
        }
    }
}
