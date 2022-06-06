namespace FBT.WebAPI.Features.Incomes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared;

    public interface IIncomeService
    {
        Task<IEnumerable<IncomeResponseModel>> GetAll();

        Task<int> Create(CreateIncomeModel model);

        Task<bool> Update(UpdateIncomeModel model);
    }
}
