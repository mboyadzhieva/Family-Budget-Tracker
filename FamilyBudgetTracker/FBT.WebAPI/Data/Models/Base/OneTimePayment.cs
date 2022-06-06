namespace FBT.WebAPI.Data.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class OneTimePayment : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
