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
         
            Company = GetAllCompanies();
           
            Employees = GetAllEmployees();

        }

        public DepartmentModel(Guid id)
            : this()
        {
            var department=_departmentManagementService.GetDepartment(id);

            this.Id = department.Id;
            this.DepartmentName = department.Name;
            if (department.Company != null)
            {
                this.CompanyId= department.Company.Id;
            }
            if (department.Employees != null)
            {
                this.DepartmentHeadId = department.DepartmentHead.Id;
            }  
        }
        public List<Company> GetAllCompanies()
        {
            _companyManagementService = new CompanyManagementService();
            return _companyManagementService.GetAllCompanies();
        }
        public List<Employee> GetAllEmployees()
        {
            _employeeManagementService = new EmployeeManagementService();
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
        public void EditDepartment(Guid id)
        {
            _departmentManagementService.EditDepartment(id,DepartmentName, CompanyId, DepartmentHeadId);
        }
        internal void DeleteDepartment(Guid? id)
        {
            if (id.HasValue)
            {
                _departmentManagementService.DeleteDepartment(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }
    }
}