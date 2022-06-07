namespace FBT.WebAPI.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Base;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class FamilyBudgetTrackerDbContext : IdentityDbContext<User>
    {
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<RecurringExpense> RecurringExpenses { get; set; }

        public DbSet<RecurringIncome> RecurringIncomes { get; set; }

        public FamilyBudgetTrackerDbContext(
            DbContextOptions<FamilyBudgetTrackerDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Expense>()
                .HasQueryFilter(e => !e.IsDeleted);

            builder
                .Entity<Income>()
                .HasQueryFilter(i => !i.IsDeleted);

            builder
                .Entity<RecurringExpense>()
                .HasQueryFilter(re => !re.IsDeleted);

            builder
                .Entity<RecurringIncome>()
                .HasQueryFilter(ri => !ri.IsDeleted);

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInfo()
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    if (entry.Entity is DeletableEntity deletableEntity)
                    {
                        if (entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;
                        }
                    }
                });
        }
    }
}
