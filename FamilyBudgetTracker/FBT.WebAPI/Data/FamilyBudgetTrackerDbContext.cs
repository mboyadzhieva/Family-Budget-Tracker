namespace FBT.WebAPI.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FamilyBudgetTrackerDbContext : IdentityDbContext<User>
    {
        public FamilyBudgetTrackerDbContext(
            DbContextOptions<FamilyBudgetTrackerDbContext> options)
            : base(options)
        {
        }
    }
}
