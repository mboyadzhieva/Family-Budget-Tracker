﻿namespace FBT.WebAPI.Data.Models
{
    using Base;

    public class Expense : OneTimePayment
    {
        public ExpenseType ExpenseType { get; set; }
    }
}
