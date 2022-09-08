using System;
using SchoolApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Web.Model.Faculty
{
    public class FacultyViewModel
    {


        public FacultyViewModel()
        {
            //FacultyViewData = new System.Data.DataTable();

        }
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "*"), MaxLength(30)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*"), MaxLength(30)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Phone { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime DOJ { get; set; }
        [Required(ErrorMessage = "*")]
        public bool active { get; set; }


        //public System.Data.DataTable FacultyViewData { get; set; }
    }
}