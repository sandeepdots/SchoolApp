
using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.FacultyService
{
    public interface IFacultyServices
    {

        List<FacultyMaster> GetFaculty();
        FacultyMaster SaveFaculty(FacultyMaster employee);
        bool DeleteFaculty(int id);
        FacultyMaster FindById(int id);
        FacultyMaster GetFacultyPresenter(int id);
        PagedListResult<FacultyMaster> GetFacultyList(SearchQuery<FacultyMaster> query, out int totalItems);

        FacultyMaster SaveFacultyPresenter(FacultyMaster student);
        FacultyMaster UpdateFacultyPresenter(FacultyMaster student);
        int DeleteFacultyPresenter(int id);
      
    }
}



