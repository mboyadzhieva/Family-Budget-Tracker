namespace FBT.WebAPI.Features.RecurringIncomes
{
    using System.Threading.Tasks;

    public interface IRecurringIncomeService
    {
        Task<int> Create(RecurringIncomeRequestModel model);
    }
}
