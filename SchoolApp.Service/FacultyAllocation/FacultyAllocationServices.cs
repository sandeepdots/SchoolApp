using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using SchoolApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.FacultyAllocation
{
     public class FacultyAllocationServices:IFacultyAllocationServices
    {


        private readonly IRepository<FacultyClassAllocation> _FacultyAllocationrRepo;

        public FacultyAllocationServices(IRepository<FacultyClassAllocation> FacultyAllocationRepo)
        {
            _FacultyAllocationrRepo = FacultyAllocationRepo;

        }
        public List<FacultyClassAllocation> GetFacultyAllocation()
        {
            return _FacultyAllocationrRepo.Query().Get().ToList();
        }

        public IEnumerable<FacultyClassAllocation> GetFacultyAllocationPresenters()
        {
            return _FacultyAllocationrRepo.Query().Get();
        }

        public bool DeleteFacultyAllocation(int id)
        {
            try
            {
                _FacultyAllocationrRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public FacultyClassAllocation SaveFacultyAllocation(FacultyClassAllocation facultyallocationmaster)
        {
            if (facultyallocationmaster.FacultyAllocationId > 0)
            {
                _FacultyAllocationrRepo.Update(facultyallocationmaster);
            }
            else
            {
                _FacultyAllocationrRepo.Insert(facultyallocationmaster);
            }
            return facultyallocationmaster;
        }

        public FacultyClassAllocation FindById(int id)
        {
            return _FacultyAllocationrRepo.FindById(id);
        }
        public FacultyClassAllocation GetFacultyAllocationPresenter(int id)
        {
            return _FacultyAllocationrRepo.FindById(id);
        }
        public FacultyClassAllocation SaveFacultyAllocationPresenter(FacultyClassAllocation facultyallocation)
        {
            _FacultyAllocationrRepo.Insert(facultyallocation);
            return facultyallocation;
        }

        public FacultyClassAllocation UpdateFacultyAllocationPresenter(FacultyClassAllocation facultyallocation)
        {
            _FacultyAllocationrRepo.Update(facultyallocation);
            return facultyallocation;
        }

        public int DeleteFacultyAllocationPresenter(int id)
        {
            _FacultyAllocationrRepo.Delete(id);
            return id;
        }


        public PagedListResult<FacultyClassAllocation> GetFacultyAllocationList(SearchQuery<FacultyClassAllocation> query, out int totalItems)
        {
            return _FacultyAllocationrRepo.Search(query, out totalItems);
        }

        public List<FacultyClassAllocation> GetFacultyAllocationById(int Id)
        {
            return _FacultyAllocationrRepo.Query().Include(a => a.ClassMaster).Include(a => a.FacultyMaster).Include(a => a.SubjectMaster).Filter(a=>a.FacultyId==Id && a.IsActive==true).Get().ToList();
        }
    }
}



