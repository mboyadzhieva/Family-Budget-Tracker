namespace FBT.WebAPI.Features.RecurringExpenses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared;

    public interface IRecurringExpenseService
    {
        Task<IEnumerable<ExpenseModel>> GetAll();

        Task<ExpenseModel> Get(int id);

        Task<int> Create(CreateExpenseModel model);

        Task<bool> Update(ExpenseModel model);
    }
}
