using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    class DepartmentUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private BranchRepository _branchRepository { get; set; }

        public BranchUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _branchRepository = new BranchRepository(_context);
        }

        public BranchRepository BranchRepository
        {
            get
            {
                return _branchRepository;
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
