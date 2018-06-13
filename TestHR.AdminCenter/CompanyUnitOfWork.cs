using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class CompanyUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private CompanyRepository _companyRepository { get; set; }

        public CompanyUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _companyRepository = new CompanyRepository(_context);
        }

        public CompanyRepository CompanyRepository
        {
            get
            {
                return _companyRepository;
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
