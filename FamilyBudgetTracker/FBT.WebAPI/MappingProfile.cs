namespace FBT.WebAPI
{
    using AutoMapper;
    using Data.Models;
    using FBT.WebAPI.Features.Expenses;
    using FBT.WebAPI.Features.Incomes;
    using FBT.WebAPI.Features.RecurringExpenses;
    using FBT.WebAPI.Features.RecurringIncomes;
    using FBT.WebAPI.Features.Shared;
    using Features.Identity;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();

            CreateMap<Expense, ExpenseResponseModel>();
            CreateMap<ExpenseResponseModel, Expense>();

            CreateMap<Expense, CreateExpenseModel>();
            CreateMap<CreateExpenseModel, Expense>();

            CreateMap<Expense, UpdateExpenseModel>();
            CreateMap<UpdateExpenseModel, Expense>();

            CreateMap<Income, IncomeResponseModel>();
            CreateMap<IncomeResponseModel, Income>();

            CreateMap<Income, CreateIncomeModel>();
            CreateMap<CreateIncomeModel, Income>();

            CreateMap<Income, UpdateIncomeModel>();
            CreateMap<UpdateIncomeModel, Income>();

            CreateMap<RecurringExpense, ExpenseResponseModel>();
            CreateMap<ExpenseResponseModel, RecurringExpense>();

            CreateMap<RecurringExpense, CreateExpenseModel>();
            CreateMap<CreateExpenseModel, RecurringExpense>();

            CreateMap<RecurringExpense, UpdateExpenseModel>();
            CreateMap<UpdateExpenseModel, RecurringExpense>();

            CreateMap<RecurringIncome, IncomeResponseModel>();
            CreateMap<IncomeResponseModel, RecurringIncome>();

            CreateMap<RecurringIncome, CreateIncomeModel>();
            CreateMap<CreateIncomeModel, RecurringIncome>();

            CreateMap<RecurringIncome, UpdateIncomeModel>();
            CreateMap<UpdateIncomeModel, RecurringIncome>();
        }
    }
}
