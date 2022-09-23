using Newtonsoft.Json;
using SchoolApp.Data;
using SchoolApp.DataTable.Extension;
using SchoolApp.DataTable.Search;
using SchoolApp.DataTable.Sort;
using SchoolApp.Service.ClassMasterServices;
using SchoolApp.Service.FacultyAllocation;
using SchoolApp.Service.FacultyService;
using SchoolApp.Service.SubjectService;
using SchoolApp.Web.Code.Serialization;
using SchoolApp.Web.Model;
using SchoolApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class FacultyAllocationController : BaseController
    {
        // GET: FacultyAllocation

        private readonly IFacultyServices facultyServices;
        private readonly ISubjectService subjectService;
        private readonly IClassMasterService classMasterService;
        private readonly IFacultyAllocationServices facultyAllocationservices;

        public FacultyAllocationController(IFacultyServices facultyServices, ISubjectService subjectService, IClassMasterService classMasterService, IFacultyAllocationServices facultyAllocationServices)
        {
            this.facultyServices = facultyServices;
            this.subjectService = subjectService;
            this.classMasterService = classMasterService;
            this.facultyAllocationservices = facultyAllocationServices;

        }
        public ActionResult AllocationIndex()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AllocationIndex(SchoolApp.DataTable.DataTable dataTable)
        {


            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //This Methods for seraching in dataTable
            var query = new SearchQuery<FacultyClassAllocation>();

            query.IncludeProperties = "ClassMaster,FacultyMaster,SubjectMaster";

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => ((q.FacultyMaster.FirstName)+""+(q.FacultyMaster.LastName)).Contains(sSearch));

            }
            // This Methods for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {

                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyClassAllocation, string>(q => q.FacultyMaster.FirstName+""+q.FacultyMaster.LastName, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

                case 3:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyClassAllocation, string>(q => q.ClassMaster.Class, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 4:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyClassAllocation, string>(q => q.SubjectMaster.Subject, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

                default:

                    break;
            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<FacultyClassAllocation> facultyAllocation = facultyAllocationservices.GetFacultyAllocationList(query, out total).Entities;

            foreach (FacultyClassAllocation facultyallo in facultyAllocation)
            {

                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {

                    facultyallo.FacultyAllocationId.ToString(), //0
                     count.ToString(), //1
                    Convert.ToString(facultyallo.FacultyMaster?.FirstName)+" "+ Convert.ToString(facultyallo.FacultyMaster?.LastName),//2
                    Convert.ToString(facultyallo.ClassMaster?.Class),//3
                     Convert.ToString(facultyallo.SubjectMaster?.Subject),//4
                     Convert.ToString(facultyallo.Class_Start_Time), //5
                     Convert.ToString(facultyallo.Class_End_Time), //6
                    Convert.ToString(facultyallo.IsActive?"Active":"Inactive") //7

                }); 
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }


        // create method for add and edit 

        [HttpGet]
        public PartialViewResult AddUpdateAllocation(int? id)
        {
            FacultyAllocationModel model = new FacultyAllocationModel();

            var facultyList = facultyServices.GetAllFacultyList();
                var subjectList = subjectService.GetAllSubjectList();
                var classList = classMasterService.GetAllClassList();
            //var Starttime=facultyAllocationservices.


                foreach (var faculty in facultyList)
                {
                    model.FacultyList.Add(new SelectListItem
                    {
                        Text = $"{faculty.FirstName} {faculty.LastName}",
                        Value = faculty.FacultyId.ToString()
                    });
                }
                foreach (var subject in subjectList)
                {
                    model.SubjectList.Add(new SelectListItem
                    {
                        Text = subject.Subject,
                        Value = subject.SubjectId.ToString()

                    });
                }
                foreach (var classValue in classList)
                {
                    model.ClassList.Add(new SelectListItem
                    {
                        Text = classValue.Class,
                        Value = classValue.ClassId.ToString()

                    });
                }
            if (id.HasValue)
            {

                if (id != null && id > 0)
                {
                    var facultyAllocations = facultyAllocationservices.FindById(Convert.ToInt32(id));
                    model.FacultyList.Find(a => a.Value == facultyAllocations.FacultyId.ToString()).Selected = true;
                    model.ClassList.Find(a => a.Value == facultyAllocations.ClassId.ToString()).Selected = true;
                    model.SubjectList.Find(a => a.Value == facultyAllocations.SubjectId.ToString()).Selected = true;
                    model.Class_End_Time = facultyAllocations.Class_End_Time;
                    model.Class_Start_Time = facultyAllocations.Class_Start_Time;
                    model.IsActive = facultyAllocations.IsActive;
                    model.FacultyAllocationId = facultyAllocations.FacultyAllocationId;
                }
            }
            return PartialView("AsignClassFaculty", model);
        }


        [HttpPost]
        public ActionResult SaveFacultyAllocation(int? id, FacultyAllocationModel model)
        {

            try
            {

                 if (ModelState.IsValid)
                {
                    bool isExist = id.HasValue;
                    FacultyClassAllocation facultyAllocations = isExist ? facultyAllocationservices.GetFacultyAllocationPresenter(id.Value) : new FacultyClassAllocation();

                   // FacultyClassAllocation facultyAllocations = new FacultyClassAllocation();

                    if (model.FacultyAllocationId > 0)
                    {
                        
                        facultyAllocations = facultyAllocationservices.FindById(Convert.ToInt32(model.FacultyAllocationId));
                        facultyAllocations.FacultyId = model.FacultyId;
                        facultyAllocations.SubjectId = model.SubjectId;
                        facultyAllocations.ClassId = model.ClassId;
                        facultyAllocations.Class_Start_Time = model.Class_Start_Time;
                        facultyAllocations.Class_End_Time = model.Class_End_Time;
                        facultyAllocations.IsActive = model.IsActive;


                        facultyAllocationservices.UpdateFacultyAllocationPresenter(facultyAllocations);
                        //return RedirectToAction("AsignClassFaculty");
                    }
                    else
                    {
                        facultyAllocations.FacultyId = model.FacultyId;
                        facultyAllocations.SubjectId = model.SubjectId;
                        facultyAllocations.ClassId = model.ClassId;
                        facultyAllocations.Class_Start_Time = model.Class_Start_Time;
                        facultyAllocations.Class_End_Time = model.Class_End_Time;
                        facultyAllocations.IsActive = model.IsActive;
                        facultyAllocations.CreatedDate = DateTime.Now;
                        facultyAllocationservices.SaveFacultyAllocation(facultyAllocations);

                       // return RedirectToAction("AllocationIndex");
                    }

                }
                ShowSuccessMessage("Success", "Faculty save successfully");
                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("AllocationIndex"), IsSuccess = true,Data="Data Save " });
            }
            catch (Exception ex)
            {
                //return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("AllocationIndex", "FacultyAllocation") });
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });

            }
        }

        // for Delete

        [HttpGet]
        public PartialViewResult DeleteFacultyAllocation()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Size = ModalSize.Small,
                IsHeader = true,
                Message = "Are you sure want to delete this Faculty Allocation?",
                Header = new ModalHeader { Heading = "Delete Faculty Allocation" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        public ActionResult DeleteFacultyAllocation(int id)
        {
            try
            {
                int get = facultyAllocationservices.DeleteFacultyAllocationPresenter(id);
                if (get == id)
                {
                    ShowSuccessMessage("Success", "Faculty is successfully deleted", false);
                    return RedirectToAction("AllocationIndex");
                }
                else
                {
                    return RedirectToAction("AllocationIndex");

                }
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }

        }


        [HttpGet]
        public ActionResult GetFacultyDasboard(int? id)
        {
            List<FacultyAllocationModel> model = new List<FacultyAllocationModel>();
 
            if (id.HasValue)
            {

                if (id != null && id > 0)
                {
                    var facultyAllocations = facultyAllocationservices.GetFacultyAllocationById(Convert.ToInt32(id));

                    foreach (var item in facultyAllocations)
                    {
                        FacultyAllocationModel obj = new FacultyAllocationModel();
                        obj.Class_End_Time = item.Class_End_Time;
                        obj.Class_Start_Time = item.Class_Start_Time;
                        obj.IsActive = item.IsActive;
                        obj.FacultyAllocationId = item.FacultyAllocationId;
                        obj.FacultyName = item.FacultyMaster?.FirstName + " "+ item.FacultyMaster?.LastName;
                        obj.ClassName = item.ClassMaster?.Class ;
                        obj.SubjectName = item.SubjectMaster?.Subject;
                        model.Add(obj);
                    }


                }
            }
            return View(model);
        }

    }
}




 