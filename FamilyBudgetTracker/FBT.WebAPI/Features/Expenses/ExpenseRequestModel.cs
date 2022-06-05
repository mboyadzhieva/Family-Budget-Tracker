namespace FBT.WebAPI.Features.Expenses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseRequestModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
                
        public string ExpenseType { get; set; }

    }
}
