using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.StudentService
{
    public interface IStudentServices
    {
        List<Student> GetStudent();
        Student SaveStudent(Student employee);
        bool DeleteStudent(int id);
        Student FindById(int id);
        Student GetStudentPresenter(int id);
        PagedListResult<Student> GetStudentList(SearchQuery<Student> query, out int totalItems);

        Student SaveStudentPresenter(Student student);
        Student UpdateStudentPresenter(Student student);
        int DeleteStudentPresenter(int id);
        Attendance GetPresenter(int id);
        
    }
}
