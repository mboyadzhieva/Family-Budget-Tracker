namespace FBT.WebAPI.Features.RecurringIncomes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared;

    public interface IRecurringIncomeService
    {
        Task<IEnumerable<IncomeModel>> GetAll();

        Task<IncomeModel> Get(int id);

        Task<int> Create(CreateIncomeModel model);

        Task<bool> Update(IncomeModel model);
    }
}
