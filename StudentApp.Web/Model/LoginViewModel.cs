using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Model
{
    public class LoginViewModel
    {
            [Required(ErrorMessage = "Email is required")]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }
        }
    }
