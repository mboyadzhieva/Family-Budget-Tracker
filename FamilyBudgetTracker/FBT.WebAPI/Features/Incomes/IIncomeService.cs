namespace FBT.WebAPI.Features.Incomes
{
    using System.Threading.Tasks;

    public interface IIncomeService
    {
        Task<int> Create(IncomeRequestModel model);
    }
}
