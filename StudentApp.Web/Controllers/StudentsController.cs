﻿using Newtonsoft.Json;
using SchoolApp.Data;
using SchoolApp.DataTable.Extension;
using SchoolApp.DataTable.Search;
using SchoolApp.DataTable.Sort;
using SchoolApp.Service.AttendanceService;
using SchoolApp.Service.StudentService;
using SchoolApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class StudentsController : BaseController
    {
        private readonly IStudentServices _studentServices;
        private readonly IAttendanceServices _attendanceServices;

        public StudentsController(IStudentServices studentServices, IAttendanceServices attendanceServices)
        {
            this._studentServices = studentServices;
            this._attendanceServices = attendanceServices;
        }
        //Load Page
        [HttpGet]
        public ActionResult Index(int? id)
        {
            StudentsViewModel model = new StudentsViewModel();

            if (id != null)
            {
                Student student = _studentServices.FindById(id.Value);
                model = new StudentsViewModel
                {
                    Student = _studentServices.GetStudent().Select(x => new SelectListItem
                    {
                        Value = x.StudentId.ToString(),
                        Text = x.Name.ToString()
                    }).OrderBy(x => x.Text).ToList()
                };
            }
            else
                model = new StudentsViewModel
                {
                    Student = _studentServices.GetStudent().Select(x => new SelectListItem
                    {
                        Value = x.StudentId.ToString(),
                        Text = x.Name.ToString()
                    }).OrderBy(x => x.Text).ToList()
                };
            model.StudentAttendance = GetData(DateTime.Now.AddMonths(-1), DateTime.Now, 0);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(StudentsViewModel model)
        {

            return View(model);
        }

        public ActionResult StudentIndex() {
            return View();
        }

        /// <summary>
        /// Refresh Table
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetTableData(DateTime? StartDate, DateTime? EndDate, int? StudentId)
        {
            StudentsViewModel model = new StudentsViewModel();
            //SchoolAppEntities db = new SchoolAppEntities();
            if (!(StudentId > 0))
            {
                StudentId = 0;
            }

            if (!StartDate.HasValue)
            {
                StartDate = DateTime.Now.AddMonths(-1);
            }

            if (!EndDate.HasValue)
            {
                EndDate = DateTime.Now;
            }

            model.StudentAttendance = GetData(StartDate, EndDate, StudentId);

            return PartialView("AttendanceDetails", model);
        }


        private System.Data.DataTable GetData(DateTime? StartDate, DateTime? EndDate, int? StudentId)
        {

            SqlConnection con = new SqlConnection("Data Source=ds252.projectstatus.co.uk;initial catalog=SchoolApp;User ID=schoolapp;Password=schoolapp@909");
            using (SqlCommand cmd = new SqlCommand("Demo1", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }
        }

        /// <summary>
        /// Method to add students attendance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult AddEditAttendance()
        {
            AddAttendance model = new AddAttendance();
            model.AttendanceDate = DateTime.Now;
            List<AttendanceObject> listAttendance = new List<AttendanceObject>();


            SchoolAppEntities SchoolAppEntities = new SchoolAppEntities();
            model.Students = SchoolAppEntities.Students.ToList();
            if (model.Students.Any())
            {
                foreach (var student in model.Students)
                {
                    AttendanceObject obj = new AttendanceObject();
                    obj.StudentId = student.StudentId;
                    obj.Name = student.Name;
                    listAttendance.Add(obj);
                }
            }


            return PartialView("AddEditAttendance", model);
        }

        [HttpPost]
        public ActionResult SubmitAttendance(AddAttendance model)
        {
            if (!string.IsNullOrEmpty(model.HdnAttendanceData))
            {
                var dataFilter = JsonConvert.DeserializeObject<List<AttendanceObject>>(model.HdnAttendanceData);
                if (dataFilter.Any())
                {

                    SchoolAppEntities dbcontext = new SchoolAppEntities();
                    //delete duplicate attendance

                    dbcontext.Attendances.RemoveRange(dbcontext.Attendances.Where(x => x.Date == model.AttendanceDate.Date));
                    dbcontext.SaveChanges();
                    //End Delete



                    List<Attendance> listAttendance = new List<Attendance>();
                    foreach (var studentData in dataFilter)
                    {
                        Attendance attendance = new Attendance();
                        attendance.StudentId = studentData.StudentId;
                        attendance.SessionFirst = studentData.SessionFirstAttendance ? "P" : "A";
                        attendance.SessionSecond = studentData.SessionSecondAttendance ? "P" : "A";
                        attendance.Date = model.AttendanceDate;
                        dbcontext.Attendances.Add(attendance);
                    }

                    dbcontext.SaveChanges();

                }

            }
            return null;

        }

       

        #region [Get Student Data]

        [HttpPost]
        public ActionResult GetStudentData(SchoolApp.DataTable.DataTable dataTable)
        {
            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //for seraching in dataTable
            var query = new SearchQuery<Student>();

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => q.Name.Contains(sSearch));
            }
            //for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {
                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<Student, string>(q => q.Name, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 3:
                    query.AddSortCriteria(new ExpressionSortCriteria<Student, string>(q => q.FatherName, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 5:
                    query.AddSortCriteria(new ExpressionSortCriteria<Student, string>(q => q.Email, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 6:
                    query.AddSortCriteria(new ExpressionSortCriteria<Student, bool>(q => q.IsActive, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

                default:
                    query.AddSortCriteria(new ExpressionSortCriteria<Student, DateTime?>(q => q.UpdatedDate, SortDirection.Ascending));
                    break;
            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<Student> students = _studentServices.GetStudentList(query, out total).Entities;
            
            foreach (Student student in students)
            {
     
               
                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    student.StudentId.ToString(), //0
                    count.ToString(),//1
                    student.Name,//2
                    student.FatherName,//3
                    !string.IsNullOrEmpty(student.Class)?$"{student.Class} / {student.Section}":"NA",//4
                    student.DOB.Value.ToString("MM/dd/yyyy"),//5
                    student.Gender,//6
                    "CSE",//7
                    $"Email: {student.Email} Mob: {student.Phone}",//8
                    student.IsActive?"Active":"Inactive" //9
                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }


        #endregion



    }
}

    
   

