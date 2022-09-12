using System.ComponentModel.DataAnnotations;


namespace SchoolApp.Web.Model
{
   
    public class RegistrationViewModel
    {
       [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Retypepassword is required.")]
        public string Retypepassword { get; set; }

    }
}