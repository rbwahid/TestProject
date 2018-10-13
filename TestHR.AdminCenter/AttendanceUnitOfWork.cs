using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class AttendanceUnitOfWork:IDisposable
    {
        private  AdminCenterDbContext _context { get; set; }
        private AttendanceRepository _attendanceRepository { get; set; }

        public AttendanceUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public AttendanceRepository AttendanceRepository
        {
            get
            {
                return _attendanceRepository;
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
