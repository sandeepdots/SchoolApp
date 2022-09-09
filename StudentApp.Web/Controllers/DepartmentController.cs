using SchoolApp.Data;
using SchoolApp.DataTable.Extension;
using SchoolApp.DataTable.Search;
using SchoolApp.DataTable.Sort;
using SchoolApp.Service.DepartmentServices;
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
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            this._departmentServices = departmentServices;
           
        }
       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    

        [HttpPost]
        public ActionResult Index(SchoolApp.DataTable.DataTable dataTable)
        {
            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //This Methods for seraching in dataTable
            var query = new SearchQuery<DepartmentMaster>();

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => q.DepartmentName.Contains(sSearch));
            }
            // This Methods for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {
                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<DepartmentMaster, string>(q => q.DepartmentName, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 3:
                    query.AddSortCriteria(new ExpressionSortCriteria<DepartmentMaster, string>(q => q.DepartmentDescription, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                case 4:
                    query.AddSortCriteria(new ExpressionSortCriteria<DepartmentMaster, DateTime>(q => q.CreatedOn, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
          
                default:
                    query.AddSortCriteria(new ExpressionSortCriteria<DepartmentMaster, DateTime?>(q => q.UpdateOn, SortDirection.Ascending));
                    break;
            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<DepartmentMaster> departmentMasters = _departmentServices.GetDepartmentList(query, out total).Entities;

            foreach (DepartmentMaster department in departmentMasters)
            {


                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    department.DepartmentId.ToString(), //0
                    count.ToString(),//1
                    department.DepartmentName,//2
                    department.DepartmentDescription,//3
                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }

        [HttpGet]
        public PartialViewResult AddUpdateDepartment(int id=0) {
            DepartmentViewModel model = new DepartmentViewModel();
            DepartmentMaster deptMaster = new DepartmentMaster();

            if (id > 0) {
                deptMaster = _departmentServices.FindById(id);
            }
            model.DepartmentId = id > 0 ? id : 0;
            model.DepartmentName = deptMaster.DepartmentName;
            model.DepartmentDescription = deptMaster.DepartmentDescription;

            return PartialView("_AddEditDepartMent", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDepartment(DepartmentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DepartmentMaster departmentMaster = new DepartmentMaster();

                    if (model.DepartmentId > 0)
                    {
                        departmentMaster = _departmentServices.FindById(model.DepartmentId);
                        departmentMaster.UpdateOn = DateTime.Now;
                    }
                    else
                    {
                        departmentMaster.CreatedOn = DateTime.Now;
                        departmentMaster.UpdateOn = DateTime.Now;
                    }

                    departmentMaster.DepartmentName = model.DepartmentName?.Trim() ?? "";
                    departmentMaster.DepartmentDescription = model.DepartmentDescription?.Trim() ?? "";
                    departmentMaster.IsActive = model.IsActive;
                    _departmentServices.SaveDepartment(departmentMaster);

                }

                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("Index", "Departement") });
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("Index", "Departement") });
            }

        }
            [HttpGet]
            public PartialViewResult DeleteDepartment()
            {
                return PartialView("_ModalDelete", new Modal
                {
                    Size = ModalSize.Small,
                    IsHeader = true,
                    Message = "Are you sure want to delete this Department?",
                    Header = new ModalHeader { Heading = "Delete Department" },
                    Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
                });
            }

            [HttpPost]
            public ActionResult DeleteDepartment(int id)
            {
                try
                {
                    int get = _departmentServices.DeleteDepartmentPresenter(id);
                    if (get == id)
                    {
                        ShowSuccessMessage("Success", "Department is successfully deleted", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Data Deleted" });
                    }
                    else
                    {
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Error Occourd" });
                    }

                }
                catch (Exception ex)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
                }

            }



        
    }
}
