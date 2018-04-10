using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class DepartmentManagementService
    {
        private AdminCenterDbContext _context;
        private DepartmentUnitOfWork _departmentUnitOfWork;
        private CompanyUnitOfWork _companyUnitOfWork;

        public DepartmentManagementService()
        {
            _context = new AdminCenterDbContext();
            _departmentUnitOfWork = new DepartmentUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
        }
        public void AddDepartment(string name, Guid companyId, Guid departmentHeadId)
        {
            var department = new Department
            {
                Company = new Company(),
                DepartmentHead = new Employee()
            };

            department.Name = name;
            department.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId);
            department.DepartmentHead.Id = departmentHeadId;

            _departmentUnitOfWork.DepartmentRepository.Add(department);
            _departmentUnitOfWork.Save();

        }
    }
}
