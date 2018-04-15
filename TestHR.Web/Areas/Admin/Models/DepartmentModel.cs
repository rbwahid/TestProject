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
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public Guid DepartmentHeadId { get; set; }
        public List<Company> Company { get; set; }  
        public List<Employee> Employees { get; set; }
        public Guid CompanyId { get; set; }
        public DepartmentModel()
        {
            _companyManagementService = new CompanyManagementService();
            _departmentManagementService = new DepartmentManagementService();
            Company = GetAllCompanies();
        }
        public List<Company> GetAllCompanies()
        {

            return _companyManagementService.GetAllCompanies();
        }
        public List<Department> GetAllDepartment()
        {
            return _departmentManagementService.GelAllDepartments();
        }
        public void AddDepartment()
        {
            _departmentManagementService.AddDepartment(DepartmentName, CompanyId, DepartmentHeadId);
        }
    }
}