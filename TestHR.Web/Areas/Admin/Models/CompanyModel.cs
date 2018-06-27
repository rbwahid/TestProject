using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using TestHR.Entities;
using TestHR.AdminCenter;

namespace TestHR.Web.Areas.Admin.Models
{
    public class CompanyModel
    {
        private CompanyManagementService _companyManagementService { get; set; }

        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters!")]
        public string Name { get; set; }
        public List<Company> MotherCompanies { get; set; }
        [DisplayName("Mother Company")]
        public Guid MotherCompanyId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 50 characters!")]
        public string Address { get; set; }
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Contact Person")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters!")]
        public string ContactPerson { get; set; }
        [Required]
        [DisplayName("Contact Person Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string ContactPersonEmail { get; set; }
        [Required]
        [DisplayName("Contact Person Phone")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string ContactPersonPhone { get; set; }
        [DisplayName("Fiscal Year Start")]
        public string FiscalYearStart { get; set; }
        public string CompanyLogo { get; set; }

        public CompanyModel()
        {
            _companyManagementService = new CompanyManagementService();
            MotherCompanies = GetAllCompanies();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyManagementService.GetAllCompanies();
        }

        public CompanyModel(Guid id)
            : this()
        {
            var company = _companyManagementService.GetCompany(id);

            this.Id = company.Id;
            this.Name = company.Name;
            if (company.MotherCompany != null)
            {
                this.MotherCompanyId = company.MotherCompany.Id;
            }
            this.Address = company.Address;
            this.Phone = company.Phone;
            this.Email = company.Email;
            this.Fax = company.Fax;
            this.ContactPerson = company.ContactPerson;
            this.ContactPersonEmail = company.ContactPersonEmail;
            this.ContactPersonPhone = company.ContactPersonPhone;
            this.FiscalYearStart = company.FiscalYearStart;

        }
        public void AddCompany()
        {
            _companyManagementService.AddCompany(Name, MotherCompanyId, Address, Phone, Fax, Email, ContactPerson, ContactPersonEmail, ContactPersonPhone, FiscalYearStart);
        }

        public void EditCompany(Guid id)
        {
            _companyManagementService.EditCompany(id,Name, MotherCompanyId, Address, Phone, Fax, Email, ContactPerson, ContactPersonEmail, ContactPersonPhone, FiscalYearStart);
        }
        internal void DeleteCompany(Guid? id)
        {
            if (id.HasValue)
            {
                _companyManagementService.DeleteCompany(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }

        public void CompanyFileImport(HttpPostedFileBase companyExcelFile)
        {
            string ExcuteMsg = string.Empty;
            int NumberOfColume = 0;
            try
            {
                //HttpPostedFileBase file = Request.Files["ProductExcel"];
                HttpPostedFileBase file = companyExcelFile;
                //Extaintion Check
                if (companyExcelFile.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
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
                        List<CompanyImportModel> companyImportList = new List<CompanyImportModel>();
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 2].Value != null &&
                                    workSheet.Cells[rowIterator, 4].Value != null)
                                {
                                    CompanyImportModel companyModel = new CompanyImportModel();
                                    companyModel.Name =
                                        workSheet.Cells[rowIterator, 2].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    companyModel.MotherCompany =
                                        workSheet.Cells[rowIterator, 3].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 3].Value.ToString();
                                    companyModel.Address =
                                        workSheet.Cells[rowIterator, 4].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 4].Value.ToString();
                                    companyModel.Phone =
                                        workSheet.Cells[rowIterator, 5].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 5].Value.ToString();
                                    companyModel.Fax =
                                        workSheet.Cells[rowIterator, 6].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 6].Value.ToString();
                                    companyModel.Email =
                                        workSheet.Cells[rowIterator, 7].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 7].Value.ToString();
                                    companyModel.ContactPerson =
                                        workSheet.Cells[rowIterator, 8].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 8].Value.ToString();
                                    companyModel.ContactPersonEmail =
                                        workSheet.Cells[rowIterator, 9].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 9].Value.ToString();
                                    companyModel.ContactPersonPhone =
                                        workSheet.Cells[rowIterator, 10].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 10].Value.ToString();
                                    companyModel.FiscalYearStart =
                                        workSheet.Cells[rowIterator, 11].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 11].Value.ToString();
                                    companyImportList.Add(companyModel);
                                }
                            }
                        }
                        //List data saving
                        if (companyImportList.Count > 0)
                        {
                            foreach (var item in companyImportList)
                            {
                                //item.companyImportList();
                                if (!string.IsNullOrEmpty(item.MotherCompany))
                                {
                                    var company = new Models.CompanyModel().GetAllCompanies().SingleOrDefault(x => x.IsDelete == false && x.Name.ToLower() == item.MotherCompany.ToLower());
                                    if (company!=null)
                                    {
                                        item.MotherCompanyId = company.Id;
                                    }
                                }
                                _companyManagementService.AddCompany(item.Name, item.MotherCompanyId, item.Address, item.Phone, item.Fax, item.Email, item.ContactPerson, item.ContactPersonEmail, item.ContactPersonPhone, item.FiscalYearStart);

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

        public Company LoadCompanyData(Guid? id)
        {
            if (id.HasValue)
            {
                return _companyManagementService.GetCompany(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }
    }

    public class CompanyImportModel:CompanyModel
    {
        public string MotherCompany { get; set; }
    }
}