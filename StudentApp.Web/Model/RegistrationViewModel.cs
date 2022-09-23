using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Web.Model
{
   
    public class RegistrationViewModel
    {
       [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Retypepassword is required.")]
        public string Retypepassword { get; set; }

    }
}
