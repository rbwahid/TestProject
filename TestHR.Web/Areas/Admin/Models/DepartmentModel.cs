using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
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
        public void DepartmentFileImport(HttpPostedFileBase departmentExcelFile)
        {
            string ExcuteMsg = string.Empty;
            int NumberOfColume = 0;
            try
            {
                //HttpPostedFileBase file = Request.Files["ProductExcel"];
                HttpPostedFileBase file = departmentExcelFile;
                //Extaintion Check
                if (departmentExcelFile.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
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
                        List<DepartmentImportModel> departmentImportList = new List<DepartmentImportModel>();
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value != null &&
                                    workSheet.Cells[rowIterator, 3].Value != null)
                                {
                                    DepartmentImportModel departmentModel = new DepartmentImportModel();
                                    departmentModel.DepartmentName =
                                        workSheet.Cells[rowIterator, 1].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    departmentModel.DepartmentHead =
                                        workSheet.Cells[rowIterator, 2].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    departmentModel.CompanyName =
                                        workSheet.Cells[rowIterator, 3].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 3].Value.ToString();
                                    departmentImportList.Add(departmentModel);
                                }
                            }
                        }
                        //List data saving
                        if (departmentImportList.Count > 0)
                        {
                            foreach (var item in departmentImportList)
                            {
                                //item.companyImportList();
                                if (!string.IsNullOrEmpty(item.CompanyName))
                                {
                                    var company = new CompanyModel().GetAllCompanies().FirstOrDefault(x => x.IsDelete == false && x.Name == item.CompanyName);
                                    if (company != null)
                                    {
                                        item.CompanyId = company.Id;
                                    }
                                }
                                if (!string.IsNullOrEmpty(item.DepartmentHead))
                                {
                                    var employee = new Models.EmployeeModel().GetAllEmployee().SingleOrDefault(x => x.IsDelete == false && x.FirstName.ToLower() == item.DepartmentHead.ToLower());
                                    if (employee != null)
                                    {
                                        item.DepartmentHeadId = employee.Id;
                                    }
                                }
                                _departmentManagementService.AddDepartment(item.DepartmentName, item.CompanyId, item.DepartmentHeadId);
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
    }

    public class DepartmentImportModel: DepartmentModel
    {
        public string CompanyName { get; set; }
        public string DepartmentHead { get; set; }
    }
}