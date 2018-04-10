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
        private DepartmentRepository _departmentRepository { get; set; }

        public DepartmentUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _departmentRepository = new DepartmentRepository(_context);
        }

        public DepartmentRepository DepartmentRepository
        {
            get
            {
                return _departmentRepository;
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
