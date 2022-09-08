using SchoolApp.Data;
using SchoolApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using SchoolApp.DataTable.Search;


namespace SchoolApp.Service.StudentService
{
   public class StudentServices : IStudentServices
    {
        private readonly IRepository<Student> _StudentRepo;
        private readonly IRepository<Attendance> _AttendanceRepo;
        public StudentServices(IRepository<Student> StudentRepo, IRepository<Attendance> AttendanceRepo)
        {
            _StudentRepo = StudentRepo;
            _AttendanceRepo = AttendanceRepo;
        }
        public List<Student> GetStudent()
        {
            return _StudentRepo.Query().Get().ToList();
        }

        public IEnumerable<Student> GetStudentPresenters()
        {
            return _StudentRepo.Query().Get();
        }

        public bool DeleteStudent(int id)
        {
            try
            {
                _StudentRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Student SaveStudent(Student student)
        {
            if (student.StudentId > 0)
            {
                _StudentRepo.Update(student);
            }
            else
            {
                _StudentRepo.Insert(student);
            }
            return student;
        }

        public Student FindById(int id)
        {
            return _StudentRepo.FindById(id);
        }
        public Student GetStudentPresenter(int id)
        {
            return _StudentRepo.FindById(id);
        }
        public Student SaveStudentPresenter(Student student)
        {
            _StudentRepo.Insert(student);
            return student;
        }

        public Student UpdateStudentPresenter(Student student)
        {
            _StudentRepo.Update(student);
            return student;
        }

        public int DeleteStudentPresenter(int id)
        {
           var attendenceId= _AttendanceRepo.Query().Filter(x => x.StudentId == id).Get().Select(x => x.AttendanceId).ToList();
            foreach (var item in attendenceId)
            {
                _AttendanceRepo.Delete(item);
            }
            _StudentRepo.Delete(id);
            return id;
        }
        public Attendance GetPresenter(int id)
        {
            return _AttendanceRepo.FindById(id);
        }

        public PagedListResult<Student> GetStudentList(SearchQuery<Student> query, out int totalItems)
        {
            return _StudentRepo.Search(query, out totalItems);
        }


    }
}
