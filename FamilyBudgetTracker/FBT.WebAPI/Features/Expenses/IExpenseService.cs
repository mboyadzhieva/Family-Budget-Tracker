namespace FBT.WebAPI.Features.Expenses
{
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseResponseModel>> GetAll();

        Task<int> Create(CreateExpenseModel model);

        Task<bool> Update(UpdateExpenseModel model);
    }
}
