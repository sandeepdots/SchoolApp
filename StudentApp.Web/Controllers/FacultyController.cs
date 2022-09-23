using Newtonsoft.Json;
using SchoolApp.Data;
using SchoolApp.DataTable.Extension;
using SchoolApp.DataTable.Search;
using SchoolApp.DataTable.Sort;
using SchoolApp.Service.FacultyService;

using SchoolApp.Web.Code.Serialization;
using SchoolApp.Web.Model.Faculty;
using SchoolApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class FacultyController : BaseController
    {

        private readonly IFacultyServices _facultyServices;
        

        public FacultyController(IFacultyServices facultyServices)
        {
            this._facultyServices = facultyServices;
          
        }

        
        // GET: FacultyMaster
        [HttpGet]
        public ActionResult FacultyIndex()
        {
            return View();

        }

        [HttpPost]
        public ActionResult FacultyIndex(SchoolApp.DataTable.DataTable dataTable)
        {


            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //for seraching in dataTable
            var query = new SearchQuery<FacultyMaster>();

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => q.FirstName.Contains(sSearch));
            }
            //for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {
                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyMaster, string>(q => q.FirstName, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 3:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyMaster, string>(q => q.LastName, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 5:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyMaster, string>(q => q.Email, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 6:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyMaster, string>(q => q.Phone, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 7:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyMaster, string>(q => q.Address, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                default:
                    query.AddSortCriteria(new ExpressionSortCriteria<FacultyMaster, decimal?>(q => q.Salary, SortDirection.Ascending));
                    break;
            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<FacultyMaster> facultyMasters = _facultyServices.GetFacultyList(query, out total).Entities;

            foreach (FacultyMaster faculty in facultyMasters)
            {


                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    faculty.FacultyId.ToString(), //0
                    count.ToString(),//1
                    faculty.FirstName,//2
                    faculty.LastName,//3
                    faculty.Phone,//4
                    faculty.Email,//5
                    faculty.Address,//6
                    faculty.Salary.ToString(),
                    faculty.DOJ.ToString(),
                    faculty.active?.ToString() ,//9
                    
                   
                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }


        // code for Edit Faculty start


        [HttpGet]

        public PartialViewResult AddEditFaculty(int? id)
        {
            FacultyViewModel model = new FacultyViewModel();
            if (id.HasValue)
            {
                FacultyMaster faculty = _facultyServices.GetFacultyPresenter(id.Value);
                model.FirstName = faculty.FirstName;
                model.LastName = faculty.LastName;
                model.Phone = faculty.Phone;
                model.Email = faculty.Email;
                model.Address = faculty.Address;
                model.Salary = faculty.Salary;
                model.DOJ = faculty.DOJ;
                model.active = faculty.active.Value;
               
               
            }
            return PartialView("AddEditFaculty", model);
        }

        [HttpPost]
        public ActionResult AddEditFaculty(int? id, FacultyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isExist = id.HasValue;
                    FacultyMaster faculty = isExist ? _facultyServices.GetFacultyPresenter(id.Value) : new FacultyMaster();
                    faculty.FirstName = model.FirstName;
                    faculty.LastName = model.LastName;
                    faculty.Phone = model.Phone;
                    faculty.Email = model.Email;
                    faculty.Address = model.Address;
                    faculty.Salary = model.Salary;
                    faculty.DOJ = model.DOJ;
                    faculty.active = model.active;
                  
                    faculty = isExist ? _facultyServices.UpdateFacultyPresenter(faculty) : _facultyServices.SaveFacultyPresenter(faculty);

                    String Msg = isExist ? "updated" : "added";
                    
                    return RedirectToAction("FacultyIndex");


            
                }
            }
            catch (Exception ex)
            {
                
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }
            return CreateModelStateErrors();
        }


        //Create Method For Delete

        [HttpGet]
        public PartialViewResult DeleteFaculty()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Size = ModalSize.Small,
                IsHeader = true,
                Message = "Are you sure want to delete this Faculty?",
                Header = new ModalHeader { Heading = "Delete Faculty" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        public ActionResult DeleteFaculty(int id)
        {
            try
            {
                int get = _facultyServices.DeleteFacultyPresenter(id)
;
                if (get == id)
                {
                    ShowSuccessMessage("Success", "Faculty is successfully deleted", false);
                    return RedirectToAction("FacultyIndex");
              
                }
                else
                {
                    return RedirectToAction("FacultyIndex");
                }

            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }

        }


    }
}






