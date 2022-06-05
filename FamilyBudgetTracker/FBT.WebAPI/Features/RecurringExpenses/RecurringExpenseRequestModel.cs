namespace FBT.WebAPI.Features.RecurringExpenses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RecurringExpenseRequestModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string ExpenseType { get; set; }
    }
}
