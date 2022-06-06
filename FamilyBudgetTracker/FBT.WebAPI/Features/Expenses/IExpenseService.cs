namespace FBT.WebAPI.Features.Expenses
{
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseModel>> GetAll();

        Task<ExpenseModel> Get(int id);

        Task<int> Create(CreateExpenseModel model);

        Task<bool> Update(ExpenseModel model);
    }
}
