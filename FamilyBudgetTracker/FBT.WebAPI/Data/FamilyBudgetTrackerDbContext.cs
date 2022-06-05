namespace FBT.WebAPI.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System;
    using FBT.WebAPI.Data.Models.Base;
    using System.Threading.Tasks;
    using System.Threading;

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
                    if (entry.Entity is Entity entity)
                    {
                        if (entry.State == EntityState.Modified 
                            && entry.Property("TotalAmount").IsModified)
                        {
                            entity.ModifiedOn = DateTime.UtcNow;
                        }
                        else if (entry.State == EntityState.Deleted)
                        {
                            entity.DeletedOn = DateTime.UtcNow;
                            entity.IsDeleted = true;

                            entry.State = EntityState.Modified;
                        }
                    }
                });
        }
    }
}
