using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class AttendanceLogUnitOfWork : IDisposable
    {
        private  AdminCenterDbContext _context { get; set; }
        private AttendanceLogRepository _attendanceLogRepository { get; set; }

        public AttendanceLogUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _attendanceLogRepository = new AttendanceLogRepository(_context);
        }

        public AttendanceLogRepository AttendanceLogRepository
        {
            get
            {
                return _attendanceLogRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
