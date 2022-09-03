using SchoolApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.AttendanceService
{
    public interface IAttendanceServices
    {
       
        Attendance UpdatePresenter(Attendance attendance);
        Attendance SavePresenter(Attendance attendance);
    }

}
