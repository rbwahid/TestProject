using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class CalendarUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private CalendarRepository _calendarRepository { get; set; }

        public CalendarUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _calendarRepository = new CalendarRepository(_context);
        }

        public CalendarRepository CalendarRepository
        {
            get
            {
                return _calendarRepository;
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
