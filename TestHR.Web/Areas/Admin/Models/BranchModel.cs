using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class BranchModel
    {
        private CompanyManagementService _companyManagementService { get; set; }
        private BranchManagementService _branchManagementService { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Company> Company { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public BranchModel()
        {
            _companyManagementService = new CompanyManagementService();
            _branchManagementService = new BranchManagementService();
            Company = GetAllCompanies();
        }
        public void BranchExcelFile(HttpPostedFileBase branchExcelFileBase)
        {

            string ExcuteMsg = string.Empty;
            int NumberOfColume = 0;
            try
            {
                //HttpPostedFileBase file = Request.Files["ProductExcel"];
                HttpPostedFileBase file = branchExcelFileBase;
                //Extaintion Check
                if (branchExcelFileBase.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
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
                        List<BranchModel> branchModelList = new List<BranchModel>();
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 2].Value != null && workSheet.Cells[rowIterator, 3].Value != null)
                                {
                                    BranchModel branchModel = new BranchModel();
                                    branchModel.CompanyName =
                                        workSheet.Cells[rowIterator, 1].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    branchModel.Name = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    branchModel.Description = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    
                                    branchModelList.Add(branchModel);
                                }
                            }
                        }
                        //List data saving
                        if (branchModelList.Count > 0)
                        {
                            string fmt = "000000000.##";
                            int lastInvoiceId = 0;
                            foreach (var item in branchModelList)
                            {
                                item.AddToBranchExcel();
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
        public void AddToBranchExcel()
        {
            _branchManagementService.AddToBranchExcel(CompanyName, Name, Description);
        }
        public List<Company> GetAllCompanies()
        {
           
            return _companyManagementService.GetAllCompanies();
        }

        public List<Branch> GetAllBranches()
        {

            return _branchManagementService.GetAllBranches();
        }

        public BranchModel(Guid id) : this()
        {
            var branch=_branchManagementService.GetBranch(id);

            this.Id = branch.Id;
            this.Name = branch.Name;
            if (branch.Company!=null)
            {
                this.CompanyId = branch.Company.Id;
            }
            this.Description = branch.Description;
       
            
        }
        public void AddBranch()
        {
            _branchManagementService.AddBranch(Name, CompanyId, Description);
        }
        public void EditBranch(Guid id)
        {
            _branchManagementService.EditBranch(id,Name, CompanyId, Description);
        }

        internal void DeleteBranch(Guid? id)
        {
            if (id.HasValue)
            {
                _branchManagementService.DeleteBranch(id.Value);
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