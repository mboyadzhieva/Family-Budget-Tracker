﻿namespace FBT.WebAPI.Data.Models
{
    using Base;

    public class Income : OneTimePayment
    {
        public IncomeType IncomeType { get; set; }
    }
}
