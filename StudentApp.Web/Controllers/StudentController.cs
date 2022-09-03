using SchoolApp.Data;

using SchoolApp.Web.Models;
using SchoolApp.Service.AttendanceService;
using SchoolApp.Service.StudentService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace SchoolApp.Web.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentServices _studentServices;
        private readonly IAttendanceServices _attendanceServices;

        public StudentController(IStudentServices studentServices, IAttendanceServices attendanceServices)
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



        /// <summary>
        /// Refresh Table
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetTableData(DateTime? StartDate, DateTime? EndDate, int? StudentId)
        {
            StudentsViewModel model = new StudentsViewModel();
            SchoolAppEntities db = new SchoolAppEntities();
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

            SqlConnection con = new SqlConnection(@"Data Source=JAGATPALSINGH\SQLEXPRESS2017;Initial Catalog=Random;User ID=IMS;Password=admin123;MultipleActiveResultSets=True;Application Name=EntityFramework");
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
        [HttpGet]
        public ActionResult Index1()
        {
            return View();
        }

    }
}




