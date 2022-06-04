namespace FBT.MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
