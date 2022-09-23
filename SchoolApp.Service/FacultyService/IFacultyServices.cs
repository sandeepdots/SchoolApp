
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
        FacultyMaster SaveFaculty(FacultyMaster faculty);
        bool DeleteFaculty(int id);
        FacultyMaster FindById(int id);
        FacultyMaster GetFacultyPresenter(int id);
        PagedListResult<FacultyMaster> GetFacultyList(SearchQuery<FacultyMaster> query, out int totalItems);

        FacultyMaster SaveFacultyPresenter(FacultyMaster faculty);
        FacultyMaster UpdateFacultyPresenter(FacultyMaster faculty);
        int DeleteFacultyPresenter(int id);

        /// <summary>
        /// Get List of faculty: Pass true to get all members and pass false to get active member
        /// </summary>
        /// <param name="IsAllFaculty"></param>
        /// <returns></returns>
        List<FacultyMaster> GetAllFacultyList(bool IsAllFaculty = false);


    }
}



