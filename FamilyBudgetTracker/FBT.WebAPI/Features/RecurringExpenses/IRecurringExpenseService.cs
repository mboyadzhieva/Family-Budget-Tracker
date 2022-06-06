namespace FBT.WebAPI.Features.RecurringExpenses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared;

    public interface IRecurringExpenseService
    {
        Task<IEnumerable<ExpenseResponseModel>> GetAll();

        Task<int> Create(CreateExpenseModel model);

        Task<bool> Update(UpdateExpenseModel model);
    }
}
