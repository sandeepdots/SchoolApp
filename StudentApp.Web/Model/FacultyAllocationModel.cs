using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Model
{
    public class FacultyAllocationModel
    {
        public FacultyAllocationModel()
        {
         //StudentAttendance = new System.Data.DataTable();
            FacultyList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SubjectList = new List<SelectListItem>();

        }

        public int FacultyAllocationId { get; set; }
        public int?FacultyId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string FacultyName { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public System.TimeSpan Class_Start_Time { get; set; }
        public System.TimeSpan Class_End_Time { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }




        //public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public string SelectedStartValue { get; set; }
        //public IEnumerable<SelectListItem> Values
        //{
        //    get
        //    {
        //        return new[]
        //        {
        //        new SelectListItem { Value = "09:00" ,Text="09:00"},
        //        new SelectListItem { Value = "10:00" ,Text="10:00" },
        //        new SelectListItem { Value = "11:00" ,Text="11:00" },
              
        //          new SelectListItem { Value = "01:00" ,Text="01:00"},
        //           new SelectListItem { Value = "02:00",Text="02:00" },
        //            new SelectListItem { Value = "03:00",Text="03:00"},
        //             new SelectListItem { Value = "04:00" ,Text="04:00"},
        //                new SelectListItem { Value = "05:00",Text="05:00" },
        //    };
        //    }
        //}

        //public string SelectedEndValue { get; set; }
        //public IEnumerable<SelectListItem> period
        //{
        //    get
        //    {
        //        return new[]
        //        {
        //      new SelectListItem { Value = "09:00" ,Text="09:00"},
        //        new SelectListItem { Value = "10:00" ,Text="10:00" },
        //        new SelectListItem { Value = "11:00" ,Text="11:00" },

        //          new SelectListItem { Value = "01:00" ,Text="01:00"},
        //           new SelectListItem { Value = "02:00",Text="02:00" },
        //            new SelectListItem { Value = "03:00",Text="03:00"},
        //             new SelectListItem { Value = "04:00" ,Text="04:00"},
        //                new SelectListItem { Value = "05:00",Text="05:00" },
        //    };
        //    }
        //}



        public string HdnFacultyAllocation { get; set; }






     

    }
}