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
    public class EducationHistoryUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private EducationHistoryRepository _educationHistoryRepository { get; set; }

        public EducationHistoryUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _educationHistoryRepository = new EducationHistoryRepository(_context);
        }

        public EducationHistoryRepository EducationHistoryRepository
        {
            get
            {
                return _educationHistoryRepository;
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
    public class CareerHistoryUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private CareerHistoryRepository _careerHistoryRepository { get; set; }

        public CareerHistoryUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _careerHistoryRepository = new CareerHistoryRepository(_context);
        }

        public CareerHistoryRepository EducationHistoryRepository
        {
            get
            {
                return _careerHistoryRepository;
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
