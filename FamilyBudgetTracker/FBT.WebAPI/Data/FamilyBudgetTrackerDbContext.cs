namespace FBT.WebAPI.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FamilyBudgetTrackerDbContext : IdentityDbContext<User>
    {
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public FamilyBudgetTrackerDbContext(
            DbContextOptions<FamilyBudgetTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId);

            builder
                .Entity<Income>()
                .HasOne(i => i.User)
                .WithMany(u => u.Incomes)
                .HasForeignKey(i => i.UserId);

            base.OnModelCreating(builder);
        }
    }
}
