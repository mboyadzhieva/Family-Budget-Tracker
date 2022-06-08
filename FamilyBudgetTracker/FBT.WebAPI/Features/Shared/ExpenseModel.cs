namespace FBT.WebAPI.Features.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string ExpenseType { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
