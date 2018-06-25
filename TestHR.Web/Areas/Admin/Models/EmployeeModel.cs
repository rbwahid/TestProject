using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        private RoleManagementService _roleManagementService { get; set; }
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters!")]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public string FathersName { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public string MothersName { get; set; }
        public string SouseName { get; set; }
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public string PresentAddress { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public string PernamentAddress { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public string Nid { get; set; }
        public string PassportNo { get; set; }

        public int? FPId { get; set; }
        public int? CardNo { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public Guid? CompanyId { get; set; }
        public List<Company> Companies { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public Guid? BranchId { get; set; }
        public List<Branch> Branches { get; set; }
        [Required(ErrorMessage = "Field Required ")]
        public Guid? DepartmentId { get; set; }
        public List<Department> Departments { get; set; }
        public Guid? PositionId { get; set; }
        public List<Position> Positions { get; set; }
       

        public List<Employee> Employees { get; set; } 
        public Guid? ReportingToId { get; set; }
        [Required]
        [Display(Name = "Username")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password",
        ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public Guid RoleId { get; set; }
        public List<Role> Roles { get; set; }
        public List<EmployeeEducationHistory> EducationHistories { get; set; }
        public List<EmployeeCareerHistory> EmployeeCareerHistories { get; set; }


        public EmployeeModel()
        {
            _employeeManagementService=new EmployeeManagementService();
            Employees = GetAllEmployee();
            _branchManagementService = new BranchManagementService();
            Branches = GetAllBranch();
            _companyManagementService=new CompanyManagementService();
            Companies = GetAllCompanies();
            _departmentManagementService=new DepartmentManagementService();
            Departments = GetAllDepartment();
            _positionManagementService=new PositionManagementService();
            Positions = GetAllPosition();
           _roleManagementService=new RoleManagementService();
            Roles = GetAllRole();
        }

        private List<Position> GetAllPosition()
        {
            
            return _positionManagementService.GelAllPositions().Where(e=>e.IsDelete==false).ToList();
        }

        private List<Department> GetAllDepartment()
        {

            return _departmentManagementService.GelAllDepartments().Where(e => e.IsDelete == false).ToList();
        }

        private List<Branch> GetAllBranch()
        {

            return _branchManagementService.GetAllBranches().Where(e => e.IsDelete == false).ToList();
        }

        public List<Company> GetAllCompanies()
        {

            return _companyManagementService.GetAllCompanies().Where(e => e.IsDelete == false).ToList();
        }
        public List<Employee> GetAllEmployee()
        {

            return _employeeManagementService.GetAllEmployees().Where(e =>!e.IsDelete).ToList();
        }
        public List<Role> GetAllRole()
        {

            return _roleManagementService.GetAllRoles().Where(e => !e.IsDelete && e.Status!=2).ToList();
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
            this.FPId = employee.FPId;
            this.CardNo = employee.CardNo;
            if (employee.Company != null)
            {
                this.CompanyId = employee.Company.Id;
            }
            if (employee.Branch != null)
            {
                this.BranchId = employee.Branch.Id;
            }
            if (employee.Position != null)
            {
                this.PositionId = employee.Position.Id;
            }
            if (employee.Department != null)
            {
                this.DepartmentId = employee.Department.Id;
            }
            this.EducationHistories = employee.EmployeeEducationHistory.ToList();
            this.EmployeeCareerHistories = employee.EmployeeCareerHistory.ToList();

        }
        public void AddEmployee()
        {
           
            _employeeManagementService.AddEmployee(FirstName, MiddleName, LastName, FathersName, MothersName, SouseName,
                PhoneNumber, PresentAddress, PernamentAddress, Email, Religion, Nationality, Nid, PassportNo, CompanyId, BranchId, DepartmentId,
                PositionId, ReportingToId, FPId, CardNo, EducationHistories, EmployeeCareerHistories, RoleId, UserName, Password);
        } 
        public void EditEmployee()
        {
           
            _employeeManagementService.EditEmployee(Id,FirstName, MiddleName, LastName, FathersName, MothersName, SouseName,
                PhoneNumber, PresentAddress, PernamentAddress, Email, Religion, Nationality, Nid, PassportNo, CompanyId, BranchId, DepartmentId,
                PositionId, ReportingToId, FPId, CardNo, EducationHistories, EmployeeCareerHistories);

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

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}