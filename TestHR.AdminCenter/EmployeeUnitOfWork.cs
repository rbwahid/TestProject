using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    public class EmployeeUnitOfWork : IDisposable
    {
         private AdminCenterDbContext _context { get; set; }
        private EmployeeRepository _employeeRepository { get; set; }

        public EmployeeUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _employeeRepository = new EmployeeRepository(_context);
        }

        public EmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository;
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
