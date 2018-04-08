using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    class CompanyUnitOfWork
    {
        private AdminCenterDbContext _context;
        public CompanyRepository CompanyRepository { get; set; }

        public CompanyUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            CompanyRepository = new CompanyRepository(_context);
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
