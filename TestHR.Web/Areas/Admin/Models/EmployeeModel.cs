using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class EmployeeModel
    {

        private CompanyManagementService _companyManagementService { get; set; }
        private EmployeeManagementService _employeeManagementService { get; set; }
        private BranchManagementService _branchManagementService { get; set; }
        private DepartmentManagementService _departmentManagementService { get; set; }
        private PositionManagementService _positionManagementService { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string SouseName { get; set; }
        public string PhoneNumber { get; set; }
        public string PresentAddress { get; set; }
        public string PernamentAddress { get; set; }
        public string Email { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Nid { get; set; }
        public string PassportNo { get; set; }

        public List<Company> Companies { get; set; }
        public Guid? CompanyId { get; set; }

        public List<Branch> Branches { get; set; }
        public Guid? BranchId { get; set; }
        public List<Department> Departments { get; set; }
        public Guid? DepartmentId { get; set; }
        public List<Position> Positions { get; set; }
        public Guid? PositionId { get; set; }

        public List<EmployeeEducationHistory> EducationHistories { get; set; }
        public List<EmployeeCareerHistory> EmployeeCareerHistories { get; set; }


        public EmployeeModel()
        {

            _branchManagementService = new BranchManagementService();
            Companies = GetAllCompanies();
            Branches = GetAllBranch();
            Departments = GetAllDepartment();
            Positions = GetAllPosition();
        }

        private List<Position> GetAllPosition()
        {
            _positionManagementService = new PositionManagementService();
            return _positionManagementService.GelAllPositions();
        }

        private List<Department> GetAllDepartment()
        {
            _departmentManagementService = new DepartmentManagementService();
            return _departmentManagementService.GelAllDepartments();
        }

        private List<Branch> GetAllBranch()
        {
            _branchManagementService = new BranchManagementService();
            return _branchManagementService.GetAllBranches();
        }

        public List<Company> GetAllCompanies()
        {
            _companyManagementService = new CompanyManagementService();
            return _companyManagementService.GetAllCompanies();
        }

        public EmployeeModel(Guid id)
            : this()
        {
            var employee = _employeeManagementService.GetEmployee(id);

            this.FirstName = employee.FirstName;
            this.MiddleName = employee.MiddleName;
            this.LastName = employee.LastName;
            this.FathersName = employee.FathersName;
            this.MothersName = employee.MothersName;
            this.SouseName = employee.SouseName;
            this.PhoneNumber = employee.PhoneNumber;
            this.PresentAddress = employee.PresentAddress;
            this.PernamentAddress = employee.PernamentAddress;
            this.Email = employee.Email;
            this.Religion = employee.Religion;
            this.Nationality = employee.Nationality;
            this.Nid = employee.Nid;
            this.PassportNo = employee.PassportNo;


        }
        public void AddEmployee()
        {
            _employeeManagementService = new EmployeeManagementService();
            _employeeManagementService.AddEmployee(FirstName, MiddleName, LastName, FathersName, MothersName, SouseName,
                PhoneNumber, PresentAddress, PernamentAddress, Email, Religion, Nationality, Nid, PassportNo, CompanyId, BranchId, DepartmentId,
                PositionId, EducationHistories, EmployeeCareerHistories);

        }

        internal void DeleteEmployee(Guid? id)
        {
            if (id.HasValue)
            {
                _employeeManagementService.DeleteEmployee(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }

        public Branch LoadBranchData(Guid? id)
        {
            if (id.HasValue)
            {
                return _branchManagementService.GetBranch(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }
    }
}