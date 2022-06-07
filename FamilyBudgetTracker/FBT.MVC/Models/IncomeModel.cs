namespace FBT.MVC.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IncomeModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Income Type")]
        public string IncomeType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        public bool IsRecurring { get; set; }
    }
}
