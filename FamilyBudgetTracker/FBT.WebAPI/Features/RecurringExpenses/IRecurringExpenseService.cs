namespace FBT.WebAPI.Features.RecurringExpenses
{
    using System.Threading.Tasks;

    public interface IRecurringExpenseService
    {
        Task<int> Create(RecurringExpenseRequestModel model);
    }
}
