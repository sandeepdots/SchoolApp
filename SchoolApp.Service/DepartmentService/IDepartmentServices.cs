using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.DepartmentService
{
    public interface IDepartmentServices
    {
        List<DepartmentMaster> GetDepartment();
        DepartmentMaster SaveDepartment(DepartmentMaster departmentMaster);
        bool DeleteDepartment(int id);
        DepartmentMaster FindById(int id);
        DepartmentMaster GetDepartmentPresenter(int id);
        PagedListResult<DepartmentMaster> GetDepartmentList(SearchQuery<DepartmentMaster> query, out int totalItems);

        DepartmentMaster SaveDepartmentPresenter(DepartmentMaster departmentMaster);
        DepartmentMaster UpdateDepartmentPresenter(DepartmentMaster departmentMaster);
        int DeleteDepartmentPresenter(int id);
      

    }
}


