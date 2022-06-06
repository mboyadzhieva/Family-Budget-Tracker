namespace FBT.WebAPI.Features.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateIncomeModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string IncomeType { get; set; }

        public bool IsRecurring { get; set; }
    }
}
