using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolApp.Web.Models
{
    public class StudentsViewModel
    {
        public StudentsViewModel()
        {
            StudentAttendance = new System.Data.DataTable();
            Student = new List<SelectListItem>();
        }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "FatherName is required.")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "MotherName is required.")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "DOB is required.")]
        public Nullable<System.DateTime> DOB { get; set; }
        [Required(ErrorMessage = "DepartmentId  is required.")]
        public Nullable<int> DepartmentId { get; set; }
        [Required(ErrorMessage = "Class is required.")]
        public string Class { get; set; }
        [Required(ErrorMessage = "Section is required.")]
        public string Section { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }
        public List<SelectListItem> Student { get; set; }
        public System.Data.DataTable StudentAttendance { get; set; }
    }
}