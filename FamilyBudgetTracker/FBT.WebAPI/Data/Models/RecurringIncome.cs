namespace FBT.WebAPI.Data.Models
{
    using Base;

    public class RecurringIncome : RecurringPayment
    {
        public decimal TotalAmount { get; set; }

        public IncomeType IncomeType { get; set; }
    }
}
