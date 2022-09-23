using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Web.Mvc;

namespace SchoolApp.Web.Model
{
   
    public class RegistrationViewModel
    {
        public RegistrationViewModel()
        {
            RoleList = new List<SelectListItem>();
        }
        public string UserName { get; set; }
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("IsAlreadySignedUpStudent", "Registration", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]
        public string EmailId { get; set; }
        //[RegularExpression(@"^.{6,}$",
        //ErrorMessage = "Password should be minimum of 6 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Retypepassword { get; set; }

        public  bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }
        public DateTime Updatedon { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public List<SelectListItem> RoleList { get; set; }

        //public List<SelectListItem> ClassList { get; set; }

    }
}