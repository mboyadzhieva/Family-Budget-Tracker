namespace FBT.WebAPI.Features.RecurringExpenses
{
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecurringExpenseService
    {
        Task<IEnumerable<ExpenseModel>> GetAll();

        Task<ExpenseModel> Get(int id);

        Task<int> Create(CreateExpenseModel model);

        Task<bool> Update(ExpenseModel model);

        Task<bool> Delete(int id);
    }
}
