using SchoolApp.Data;
using SchoolApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.AttendanceService
{
    public class AttendanceServices : IAttendanceServices
    {
        
        private readonly IRepository<Attendance> _AttendanceRepo;

        public AttendanceServices(IRepository<Attendance> AttendanceRepo)
        {
            _AttendanceRepo = AttendanceRepo;
        }

        public Attendance SavePresenter(Attendance attendance)
        {
            _AttendanceRepo.Insert(attendance);
            return attendance;
        }

        public Attendance UpdatePresenter(Attendance attendance)
        {
            _AttendanceRepo.Update(attendance);
            return attendance;
        }
    }
}
