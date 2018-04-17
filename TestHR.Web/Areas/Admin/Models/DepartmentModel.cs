using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class DepartmentModel
    {
        private CompanyManagementService _companyManagementService { get; set; }
        private DepartmentManagementService _departmentManagementService { get; set; }
        private EmployeeManagementService _employeeManagementService { get; set; }
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public Guid DepartmentHeadId { get; set; }
        public List<Company> Company { get; set; }  
        public List<Employee> Employees { get; set; }
        public Guid CompanyId { get; set; }
        public DepartmentModel()
        {
            
            _departmentManagementService = new DepartmentManagementService();
            _companyManagementService = new CompanyManagementService();
            Company = GetAllCompanies();
            _employeeManagementService = new EmployeeManagementService();
            Employees = GetAllEmployees();

        }
        public List<Company> GetAllCompanies()
        {

            return _companyManagementService.GetAllCompanies();
        }
        public List<Employee> GetAllEmployees()
        {

            return _employeeManagementService.GetAllEmployees();
        }
        public List<Department> GetAllDepartments()
        {
            return _departmentManagementService.GelAllDepartments();
        }
        public void AddDepartment()
        {
            _departmentManagementService.AddDepartment(DepartmentName, CompanyId, DepartmentHeadId);
        }
    }
}