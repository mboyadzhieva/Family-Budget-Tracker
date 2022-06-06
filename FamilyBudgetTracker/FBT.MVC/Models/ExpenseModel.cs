using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBT.MVC.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Expense Type")]
        public string ExpenseType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }
    }
}
