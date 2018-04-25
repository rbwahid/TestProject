using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class CalenderUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private CalenderRepository _calenderRepository { get; set; }

        public CalenderUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _calenderRepository = new CalenderRepository(_context);
        }

        public CalenderRepository CalenderRepository
        {
            get
            {
                return _calenderRepository;
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
