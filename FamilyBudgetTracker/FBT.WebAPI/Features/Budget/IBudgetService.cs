namespace FBT.WebAPI.Features.Budget
{
    using System.Threading.Tasks;

    public interface IBudgetService
    {
        decimal Calculate(string userId);
    }
}
