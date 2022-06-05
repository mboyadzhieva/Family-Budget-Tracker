namespace FBT.WebAPI.Data.Models
{
    public class Expense : BasePayment
    {
        public ExpenseType ExpenseType { get; set; }
    }
}
