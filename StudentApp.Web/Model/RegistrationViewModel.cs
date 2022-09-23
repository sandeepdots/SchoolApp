using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Model
{
    public class RegistrationViewModel
    {
        public RegistrationViewModel()
        {
            RoleList = new List<SelectListItem>();
        }
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SaltKey { get; set; }
        public int? RoleId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> Updatedon { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}