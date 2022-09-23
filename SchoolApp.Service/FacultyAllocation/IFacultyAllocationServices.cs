using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.FacultyAllocation
{
    public interface IFacultyAllocationServices
    {
        List<FacultyClassAllocation> GetFacultyAllocation();
        FacultyClassAllocation SaveFacultyAllocation(FacultyClassAllocation facultyallocation);
        bool DeleteFacultyAllocation(int id);
        FacultyClassAllocation FindById(int id);
        FacultyClassAllocation GetFacultyAllocationPresenter(int id);
        PagedListResult<FacultyClassAllocation> GetFacultyAllocationList(SearchQuery<FacultyClassAllocation> query, out int totalItems);
        List<FacultyClassAllocation> GetFacultyAllocationById(int Id);


        FacultyClassAllocation SaveFacultyAllocationPresenter(FacultyClassAllocation facultyallocation);
        FacultyClassAllocation UpdateFacultyAllocationPresenter(FacultyClassAllocation facultyallocation);
        int DeleteFacultyAllocationPresenter(int id);
       


    }
}



