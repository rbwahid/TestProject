using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
   public class LeaveTypeUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private LeaveTypeRepository _leaveTypeRepository { get; set; }

        public LeaveTypeUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _leaveTypeRepository = new LeaveTypeRepository(_context);
        }

        public LeaveTypeRepository LeaveTypeRepository
        {
            get
            {
                return _leaveTypeRepository;
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
