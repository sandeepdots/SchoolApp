using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Model
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Passowrd")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassowrd { get; set; }
        public Guid Id { get; set; }
        public int Otp { get; set; }
        [Required(ErrorMessage = "Otp is Required")]
        [Display(Name = "Otp")]
        public int? Otpfromuser { get; set; }
    }
}