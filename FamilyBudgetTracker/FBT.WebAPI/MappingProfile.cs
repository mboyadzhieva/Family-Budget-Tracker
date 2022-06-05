namespace FBT.WebAPI
{
    using AutoMapper;
    using Data.Models;
    using FBT.WebAPI.Features.Expenses;
    using FBT.WebAPI.Features.Incomes;
    using FBT.WebAPI.Features.RecurringExpenses;
    using FBT.WebAPI.Features.RecurringIncomes;
    using Features.Identity;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();

            CreateMap<Expense, ExpenseRequestModel>();
            CreateMap<ExpenseRequestModel, Expense>();

            CreateMap<Income, IncomeRequestModel>();
            CreateMap<IncomeRequestModel, Income>();

            CreateMap<RecurringExpense, RecurringExpenseRequestModel>();
            CreateMap<RecurringExpenseRequestModel, RecurringExpense>();

            CreateMap<RecurringIncome, RecurringIncomeRequestModel>();
            CreateMap<RecurringIncomeRequestModel, RecurringIncome>();
        }
    }
}
