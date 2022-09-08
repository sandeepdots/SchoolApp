using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Model
{
    public class RegistrationViewModel
    {
       [Required(ErrorMessage = "Fullname is required.")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Retypepassword is required.")]
        public string Retypepassword { get; set; }

    }
}