using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;


namespace TestHR.AdminCenter
{
    public class AttendanceLogManagementService
    {
        private AdminCenterDbContext _context;
        private AttendanceLogUnitOfWork _attendanceLogUnitOfWork;

        public  AttendanceLogManagementService()
        {
            _context = new AdminCenterDbContext();
            _attendanceLogUnitOfWork = new AttendanceLogUnitOfWork(_context);
        }

        public List<AttendanceLog> GetAllAttendanceLog()
        {
            return _attendanceLogUnitOfWork.AttendanceLogRepository.GetAll().ToList();
        }

        public AttendanceLog GetAttendanceLog(Guid id)
        {
            return _attendanceLogUnitOfWork.AttendanceLogRepository.GetById(id);
        }
         public void DeleteAttendanceLog(Guid id)
        {
            _attendanceLogUnitOfWork.AttendanceLogRepository.DeleteById(id);
            _attendanceLogUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _attendanceLogUnitOfWork.AttendanceLogRepository.GetAll().Count();
            
        }
        public void AddToAttendanceLog(string name, DateTime attendanceDateTime,TimeSpan punchTime)
        {
            var attendanceInfo = new AttendanceLog();
            attendanceInfo.Name= name;
            attendanceInfo.AttendanceDate = attendanceDateTime;
            attendanceInfo.PunchTime = punchTime;
            _attendanceLogUnitOfWork.AttendanceLogRepository.Add(attendanceInfo);
            _attendanceLogUnitOfWork.Save();

        }
    }
}
