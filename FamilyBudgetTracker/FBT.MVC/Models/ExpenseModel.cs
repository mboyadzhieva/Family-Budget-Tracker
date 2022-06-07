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
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        //[Range(0, 9999999999999999.99)]
        //[DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }

        public bool IsRecurring { get; set; }
    }
}
