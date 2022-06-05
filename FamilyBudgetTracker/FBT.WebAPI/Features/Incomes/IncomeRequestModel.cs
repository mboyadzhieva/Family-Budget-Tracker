namespace FBT.WebAPI.Features.Incomes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IncomeRequestModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string IncomeType { get; set; }
    }
}
