using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolApp.Service.ClassMasterServices
{
    public interface IClassMasterService
    {
        List<ClassMaster> GetClassMaster();
        ClassMaster SaveClassMaster(ClassMaster classmaster);
        bool DeleteClassMaster(int id);
        ClassMaster FindById(int id);
        ClassMaster GetClassMasterPresenter(int id);
        PagedListResult<ClassMaster> GetClassMasterList(SearchQuery<ClassMaster> query, out int totalItems);

        ClassMaster SaveClassMasterPresenter(ClassMaster classmaster);
        ClassMaster UpdateClassMasterPresenter(ClassMaster classmaster);
        int DeleteClassMasterPresenter(int id);
        List<ClassMaster> GetAllClassList(bool IsAllClass = false);
    }
}
