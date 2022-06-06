namespace FBT.WebAPI
{
    using AutoMapper;
    using Data.Models;
    using Features.Identity;
    using Features.Shared;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();

            CreateMap<Expense, CreateExpenseModel>();
            CreateMap<CreateExpenseModel, Expense>();

            CreateMap<Expense, ExpenseModel>();
            CreateMap<ExpenseModel, Expense>();

            CreateMap<Income, IncomeModel>();
            CreateMap<IncomeModel, Income>();

            CreateMap<Income, CreateIncomeModel>();
            CreateMap<CreateIncomeModel, Income>();

            CreateMap<RecurringExpense, ExpenseModel>();
            CreateMap<ExpenseModel, RecurringExpense>();

            CreateMap<RecurringExpense, CreateExpenseModel>();
            CreateMap<CreateExpenseModel, RecurringExpense>();

            CreateMap<RecurringIncome, IncomeModel>();
            CreateMap<IncomeModel, RecurringIncome>();

            CreateMap<RecurringIncome, CreateIncomeModel>();
            CreateMap<CreateIncomeModel, RecurringIncome>();
        }
    }
}
