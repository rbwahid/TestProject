using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
   public class AttendanceManagementService
    {
        private AttendanceUnitOfWork _attendanceUnitOfWork;
        private AdminCenterDbContext _context;

       public AttendanceManagementService()
        {
            _context  = new AdminCenterDbContext();
            _attendanceUnitOfWork= new AttendanceUnitOfWork(_context);
        }

        public List<Attendance> GetAttendances()
        {
            return _attendanceUnitOfWork.AttendanceRepository.GetAll().ToList();
        } 
    }
}
