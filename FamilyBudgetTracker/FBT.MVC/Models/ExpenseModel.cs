namespace FBT.MVC.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Expense Type")]
        public string ExpenseType { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Do you want to add this expense as recurring?")]
        public bool IsRecurring { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
    }
}
