namespace FBT.WebAPI.Features.RecurringIncomes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RecurringIncomeRequestModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string IncomeType { get; set; }
    }
}
