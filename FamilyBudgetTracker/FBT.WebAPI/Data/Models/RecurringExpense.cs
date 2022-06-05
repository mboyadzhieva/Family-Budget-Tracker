namespace FBT.WebAPI.Data.Models
{
    using Base;

    public class RecurringExpense : RecurringPayment
    {
        public decimal TotalAmount { get; set; }

        public ExpenseType ExpenseType { get; set; }
    }
}
