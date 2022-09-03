using SchoolApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Models
{
    public class AttendanceViewModel
    {

        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public System.DateTime Date { get; set; }
        public string SessionFirst { get; set; }
        public string SessionSecond { get; set; }

        public virtual Student Student { get; set; }
        public int GetStudentPresenter { get; set; }
    }
}