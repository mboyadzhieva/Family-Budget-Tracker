namespace FBT.WebAPI.Features.Incomes
{
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIncomeService
    {
        Task<IEnumerable<IncomeModel>> GetAll();

        Task<IncomeModel> Get(int id);

        Task<int> Create(CreateIncomeModel model);

        Task<bool> Update(IncomeModel model);

        Task<bool> Delete(int id);
    }
}
