using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class ShiftUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private ShiftRepository _shiftRepository { get; set; }

        public ShiftUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _shiftRepository = new ShiftRepository(_context);
        }

        public ShiftRepository ShiftRepository
        {
            get
            {
                return _shiftRepository;
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
