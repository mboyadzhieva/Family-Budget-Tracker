namespace FBT.MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password must be between 4 and 30 characters!", MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password must be between 4 and 30 characters!", MinimumLength = 4)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
