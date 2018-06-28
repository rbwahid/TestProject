using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private EmployeeUnitOfWork _employeeUnitOfWork;

        public DepartmentManagementService()
        {
            _context = new AdminCenterDbContext();
            _departmentUnitOfWork = new DepartmentUnitOfWork(_context);
            _employeeUnitOfWork = new EmployeeUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
            _employeeUnitOfWork = new EmployeeUnitOfWork(_context);
        }
        public Department GetDepartment(Guid id)
        {
            return _departmentUnitOfWork.DepartmentRepository.GetById(id);
        }

        public void DeleteDepartment(Guid id)
        {
            _departmentUnitOfWork.DepartmentRepository.DeleteById(id);
            _departmentUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _departmentUnitOfWork.DepartmentRepository.GetAll().Count();

        }
        public List<Department> GelAllDepartments()
        {
            return _departmentUnitOfWork.DepartmentRepository.GetAll().ToList();
        }
        public void AddDepartment(string name, Guid companyId, Guid? departmentHeadId) //Not sure
        {
            var department = new Department();
          

            department.Name = name;
            department.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId);
            if (departmentHeadId.HasValue)
            {
                department.DepartmentHead = _employeeUnitOfWork.EmployeeRepository.GetById(departmentHeadId.Value);
            }
            _departmentUnitOfWork.DepartmentRepository.Add(department);
            _departmentUnitOfWork.Save();

        }

        public void EditDepartment(Guid id,string name, Guid companyId, Guid? departmentHeadId)
        {
            var department = GetDepartment(id);
            department.Name = name;
            department.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId);
            if (departmentHeadId.HasValue)
            {
                department.DepartmentHead.Id = departmentHeadId.Value;
            }
            _departmentUnitOfWork.Save();

        }
    }
}
