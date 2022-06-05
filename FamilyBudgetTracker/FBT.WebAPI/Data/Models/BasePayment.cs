namespace FBT.WebAPI.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BasePayment
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public bool IsRecurring { get; set; }

    }
}
