namespace FBT.WebAPI.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<Expense> Expenses { get; } = new HashSet<Expense>();

        public IEnumerable<Income> Incomes { get; } = new HashSet<Income>();

    }
}
