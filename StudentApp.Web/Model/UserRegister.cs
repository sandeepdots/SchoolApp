using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Web.Model
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email Name")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password Name")]
        public string Password { get; set; }
    



    } 
}

//[Required(ErrorMessage = "please Enter Name")]
//public string Name { get; set; }

//[Required(ErrorMessage = "Please Enter Last Name")]
//public string LastName { get; set; }

//[Required(ErrorMessage = "please Enter Email")]
//public string Email { get; set; }
//[Required(ErrorMessage = "please Enter Password ")]
//public string Password { get; set; }

//public string Mobile { get; set; }
//[Required(ErrorMessage = "please Enter Address")]
//public string Address { get; set; }

//public string Gender { get; set; }
//[Required(ErrorMessage = "Please select User Type")]
//public string UserType { get; set; }

//[Required(ErrorMessage = "please upload your photo")]
//public HttpPostedFileBase Upload { get; set; }

//public bool IsActive { get; set; }






