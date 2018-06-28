using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OfficeOpenXml;
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

        public int? FPId { get; set; }
        public int? CardNo { get; set; }
        public List<Company> Companies { get; set; }
        public Guid? CompanyId { get; set; }
        public string   CompanyName { get; set; }
        public List<Branch> Branches { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public List<Department> Departments { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<Position> Positions { get; set; }
        public string PositionName { get; set; }
        public Guid? PositionId { get; set; }

        public List<Employee> Employees { get; set; } 
        public Guid? ReportingToId { get; set; }
        [Required(ErrorMessage = "Userame field is required")]

        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password field is required")]
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
        // Employee Excel File //
        public void EmployeeExcelFile(HttpPostedFileBase employeeExcelFileBase)
        {

            string ExcuteMsg = string.Empty;
            int NumberOfColume = 0;
            try
            {
                //HttpPostedFileBase file = Request.Files["ProductExcel"];
                HttpPostedFileBase file = employeeExcelFileBase;
                //Extaintion Check
                if (employeeExcelFileBase.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
                    file.FileName.EndsWith("XLS") ||
                    file.FileName.EndsWith("XLSX"))
                {
                    //Null Exp Check
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        List<EmployeeModel> employeeModelList = new List<EmployeeModel>();
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 2].Value != null || workSheet.Cells[rowIterator, 3].Value != null || workSheet.Cells[rowIterator, 4].Value != null)
                                {
                                    EmployeeModel employeeModel = new EmployeeModel();
                                    employeeModel.CompanyName =
                                        workSheet.Cells[rowIterator, 1].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 1].Value.ToString();

                                    employeeModel.FirstName = workSheet.Cells[rowIterator, 2].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 2].Value.ToString();

                                    employeeModel.MiddleName = workSheet.Cells[rowIterator, 3].Value == null
                                           ? null
                                           : workSheet.Cells[rowIterator, 3].Value.ToString();

                                    employeeModel.LastName = workSheet.Cells[rowIterator, 4].Value == null
                                           ? null
                                           : workSheet.Cells[rowIterator, 4].Value.ToString();
                                    employeeModel.BranchName = workSheet.Cells[rowIterator, 5].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 5].Value.ToString();

                                    employeeModel.DepartmentName = workSheet.Cells[rowIterator, 6].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 6].Value.ToString();

                                    employeeModel.PositionName = workSheet.Cells[rowIterator, 7].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 7].Value.ToString();

                                    employeeModel.FPId = workSheet.Cells[rowIterator, 8].Value == null 
                                        ? 0
                                            : int.Parse(workSheet.Cells[rowIterator, 8].Value.ToString());

                                    employeeModel.CardNo = workSheet.Cells[rowIterator, 9].Value == null
                                            ? 0
                                            : int.Parse(workSheet.Cells[rowIterator, 9].Value.ToString());

                                    employeeModel.PhoneNumber = workSheet.Cells[rowIterator, 10].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 10].Value.ToString();

                                    employeeModel.Email = workSheet.Cells[rowIterator, 11].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 11].Value.ToString();

                                    employeeModelList.Add(employeeModel);
                                }
                            }
                        }
                        //List data saving
                        if (employeeModelList.Count > 0)
                        {

                            foreach (var item in employeeModelList)
                            {
                                item.AddToEmployeeExcel();
                            }
                        }
                    }
                }
            }
            catch (Exception r)
            {
                ExcuteMsg = r.Message;
            }
        }
        public void AddToEmployeeExcel()
        {
            _employeeManagementService.AddToEmployeeExcel(CompanyName, FirstName, MiddleName, LastName, BranchName, DepartmentName, PositionName, FPId, CardNo, PhoneNumber, Email);
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