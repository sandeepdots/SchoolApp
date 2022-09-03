using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public List<SelectListItem> Student { get; set; }
        public System.Data.DataTable StudentAttendance { get; set; }
    }
}