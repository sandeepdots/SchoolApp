using SchoolApp.Data;
using SchoolApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using SchoolApp.DataTable.Search;
using SchoolApp.Service.FacultyService;

namespace SchoolApp.Service.StudentService
{
    public class FacultyServices : IFacultyServices
    {
        private readonly IRepository<FacultyMaster> _FacultyRepo;
     
        public FacultyServices(IRepository<FacultyMaster> FacultyRepo)
        {
            _FacultyRepo = FacultyRepo;
           
        }
        public List<FacultyMaster> GetFaculty()
        {
            return _FacultyRepo.Query().Get().ToList();
        }

        public IEnumerable<FacultyMaster> GetFacultyPresenters()
        {
            return _FacultyRepo.Query().Get();
        }

        public bool DeleteFaculty(int id)
        {
            try
            {
                _FacultyRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public FacultyMaster SaveFaculty(FacultyMaster facultyMaster)
        {
            if (facultyMaster.FacultyId > 0)
            {
                _FacultyRepo.Update(facultyMaster);
            }
            else
            {
                _FacultyRepo.Insert(facultyMaster);
            }
            return facultyMaster;
        }

        public FacultyMaster FindById(int id)
        {
            return _FacultyRepo.FindById(id);
        }
        public FacultyMaster GetFacultyPresenter(int id)
        {
            return _FacultyRepo.FindById(id);
        }
        public FacultyMaster SaveFacultyPresenter(FacultyMaster facultyMaster)
        {
            _FacultyRepo.Insert(facultyMaster);
            return facultyMaster;
        }

        public FacultyMaster UpdateFacultyPresenter(FacultyMaster facultyMaster)
        {
            _FacultyRepo.Update(facultyMaster);
            return facultyMaster;
        }

        public int DeleteFacultyPresenter(int id)
        {
            _FacultyRepo.Delete(id);
            return id;
        }
        //public Attendance GetPresenter(int id)
        //{
        //    return _AttendanceRepo.FindById(id);
        //}

        public PagedListResult<FacultyMaster> GetFacultyList(SearchQuery<FacultyMaster> query, out int totalItems)
        {
            return _FacultyRepo.Search(query, out totalItems);
        }

    }
}
