using System;
using System.ComponentModel.DataAnnotations;


namespace SchoolApp.Web.Model
{

    public class RegistrationViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string SaltKey { get; set; }
        public int RoleId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public DateTime Updatedon { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
