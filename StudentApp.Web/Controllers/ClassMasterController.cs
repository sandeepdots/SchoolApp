using SchoolApp.Data;
using SchoolApp.DataTable.Extension;
using SchoolApp.DataTable.Search;
using SchoolApp.DataTable.Sort;
using SchoolApp.Service.ClassMasterServices;
using SchoolApp.Web.Code.Serialization;
using SchoolApp.Web.Model;
using SchoolApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class ClassMasterController : BaseController
    {
        private readonly IClassMasterService _classmasterServices;
        public ClassMasterController(IClassMasterService classmasterServices)
        {
            this._classmasterServices = classmasterServices;

        }
        [HttpGet]
        public ActionResult ClassViewIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ClassViewIndex(SchoolApp.DataTable.DataTable dataTable)
        {
            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //This Methods for seraching in dataTable
            var query = new SearchQuery<ClassMaster>();

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => q.Class.Contains(sSearch));
            }
            // This Methods for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {
                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<ClassMaster, string>(q => q.Class, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

                //case 3:
                //    query.AddSortCriteria(new ExpressionSortCriteria<ClassMaster, bool>(q => q.IsActive, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                //    break;
            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<ClassMaster> classMasters = _classmasterServices.GetClassMasterList(query, out total).Entities;

            foreach (ClassMaster classmaster in classMasters)
            {


                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    classmaster.ClassId.ToString(), //0
                    count.ToString(),//1
                    classmaster.Class,//2
                    classmaster.IsActive?"Active":"Inactive" //3

                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }



        /// <summary>
        /// /Edit Code here 
        /// </summary>
        /// <returns></returns>



        //Edit Add
        public PartialViewResult AddUpdateClassMaster(int id=0)
        {

            ClassMasterViewmodel model = new ClassMasterViewmodel();
            ClassMaster classMaster = new ClassMaster();


            if (id > 0)
            {
                classMaster = _classmasterServices.FindById(id);
            }
            model.ClassId = id > 0 ? id : 0;
            model.Class = classMaster.Class;
            model.IsActive = classMaster.IsActive;


            return PartialView("AddUpdateClassMaster", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveClassMaster(int? id, ClassMasterViewmodel model)

        {

            try
            {
                if (ModelState.IsValid)
                {
                    ClassMaster classMaster = new ClassMaster();
                    if (model.ClassId > 0)
                    {
                        classMaster = _classmasterServices.FindById(model.ClassId);

                    }
                    else
                    {

                    }

                    classMaster.Class = model.Class?.Trim() ?? "";
                    classMaster.IsActive = model.IsActive;

                    _classmasterServices.SaveClassMaster(classMaster);





                }
                return RedirectToAction("ClassViewIndex");

            }
            catch (Exception ex)
            {

                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }

          
        }

      

        public PartialViewResult DeleteClassMaster()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Size = ModalSize.Small,
                IsHeader = true,
                Message = "Are you sure want to delete this Class?",
                Header = new ModalHeader { Heading = "Delete Class" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        public ActionResult DeleteClassMaster(int id)
        {
            try
            {
                int get = _classmasterServices.DeleteClassMasterPresenter(id);
                if (get == id)
                {
                    ShowSuccessMessage("Success", "Class is successfully deleted", false);
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