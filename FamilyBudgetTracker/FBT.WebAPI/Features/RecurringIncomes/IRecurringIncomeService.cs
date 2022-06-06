namespace FBT.WebAPI.Features.RecurringIncomes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared;

    public interface IRecurringIncomeService
    {
        Task<IEnumerable<IncomeResponseModel>> GetAll();

        Task<int> Create(CreateIncomeModel model);

        Task<bool> Update(UpdateIncomeModel model);
    }
}
