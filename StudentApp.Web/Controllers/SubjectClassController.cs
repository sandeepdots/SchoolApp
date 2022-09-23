using SchoolApp.Data;
using SchoolApp.DataTable.Extension;
using SchoolApp.DataTable.Search;
using SchoolApp.DataTable.Sort;
using SchoolApp.Service.SubjectService;
using SchoolApp.Web.Code.Serialization;
using SchoolApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;



namespace SchoolApp.Web.Controllers
{
    public class SubjectClassController : BaseController

    {
        private readonly ISubjectService _subjectmasterService;
        public SubjectClassController(ISubjectService subjectmasertService)
        {
            this._subjectmasterService = subjectmasertService;

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
            var query = new SearchQuery<SubjectMaster>();

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => q.Subject.Contains(sSearch));
            }
            // This Methods for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {
                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<SubjectMaster, string>(q => q.Subject, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

                case 3:
                    query.AddSortCriteria(new ExpressionSortCriteria<SubjectMaster, bool>(q => q.IsActive, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<SubjectMaster> subjectmasters = _subjectmasterService.GetSubjectMasterList(query, out total).Entities;

            foreach (SubjectMaster subjectmaster in subjectmasters)
            {


                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    subjectmaster.SubjectId.ToString(), //0
                    count.ToString(),//1
                    subjectmaster.Subject,//2
                    subjectmaster.IsActive?"Active":"Inactive" //3

                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }



        // add edit method
        public PartialViewResult AddEditSubject(int id=0)
        {

            SubjectViewModel model = new SubjectViewModel();
            SubjectMaster subMaster = new SubjectMaster();
           
            if (id>0)
            {
                subMaster = _subjectmasterService.FindById(id);
            }
            model.SubjectId = id > 0 ? id : 0;
            model.Subject = subMaster.Subject;
            model.IsActive = subMaster.IsActive;

            return PartialView("AddEditSubject", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSubjectMaster(int? id, SubjectViewModel model)

        {

            try
            {
                if (ModelState.IsValid)
                {
                    SubjectMaster subjectMaster = new SubjectMaster();
                    if (model.SubjectId > 0)
                    {
                        subjectMaster = _subjectmasterService.FindById(model.SubjectId);
                     
                    }
                    else
                    {
                      
                    }

                    subjectMaster.Subject = model.Subject?.Trim() ?? "";
                    subjectMaster.IsActive = model.IsActive;
                
                    _subjectmasterService.SaveSubjectMaster(subjectMaster);

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }
          
        }

        [HttpGet]
        public PartialViewResult DeleteSubjectMaster()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Size = ModalSize.Small,
                IsHeader = true,
                Message = "Are you sure want to delete this Subject?",
                Header = new ModalHeader { Heading = "Delete Subject" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        public ActionResult DeleteSubjectMaster(int id)
        {
            try
            {
                int get = _subjectmasterService.DeleteSubjectMasterPresenter(id);
                if (get == id)
                {
                    ShowSuccessMessage("Success", "Subject is successfully deleted", false);
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

