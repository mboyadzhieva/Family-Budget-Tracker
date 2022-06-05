using System.Threading.Tasks;

namespace FBT.WebAPI.Features.Expenses
{
    public interface IExpenseService
    {
        Task<int> Create(ExpenseRequestModel model);
    }
}
