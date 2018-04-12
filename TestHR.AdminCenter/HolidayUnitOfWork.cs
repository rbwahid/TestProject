using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class HolidayUnitOfWork
    {
        private AdminCenterDbContext _context { get; set; }
        private HolidayRepository _holidayRepository { get; set; }

        public HolidayUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _holidayRepository = new HolidayRepository(_context);
        }

        public HolidayRepository HolidayRepository
        {
            get
            {
                return _holidayRepository;
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
