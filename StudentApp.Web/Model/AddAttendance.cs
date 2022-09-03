using SchoolApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Models
{
    public class AddAttendance
    {
        public AddAttendance()
        {
            ListAttendance = new List<AttendanceObject>();
        }
        [Required(ErrorMessage = "DATE IS REQUIRED")]
        public DateTime AttendanceDate { get; set; }
        public List<AttendanceObject> ListAttendance { get; set; }
        public List<Student> Students { get; set; }
        public string HdnAttendanceData { get; set; }

    }
         public class AttendanceObject {
            public int StudentId { get; set; }
            public string Name { get; set; }
            public bool SessionFirstAttendance { get; set; }
            public bool SessionSecondAttendance { get; set; }
         }

    }
