using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    public class LeaveApplicationUnitOfWork : IDisposable
    {
         private AdminCenterDbContext _context { get; set; }
        private LeaveApplicationRepository _leaveApplicationRepository { get; set; }

        public LeaveApplicationUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _leaveApplicationRepository = new LeaveApplicationRepository(_context);
        }

        public LeaveApplicationRepository LeaveApplicationRepository
        {
            get
            {
                return _leaveApplicationRepository;
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
