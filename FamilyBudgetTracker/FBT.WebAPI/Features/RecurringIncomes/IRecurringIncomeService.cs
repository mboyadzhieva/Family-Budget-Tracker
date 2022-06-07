namespace FBT.WebAPI.Features.RecurringIncomes
{
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecurringIncomeService
    {
        Task<IEnumerable<IncomeModel>> GetAll();

        Task<IncomeModel> Get(int id);

        Task<int> Create(CreateIncomeModel model);

        Task<bool> Update(IncomeModel model);

        Task<bool> Delete(int id);
    }
}
