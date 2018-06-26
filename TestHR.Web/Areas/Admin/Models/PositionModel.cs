using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class PositionModel
    {
        private CompanyManagementService _companyManagementService { get; set; }
        private PositionManagementService _positionManagementService { get; set; }
        private DepartmentManagementService _departmentManagementService { get; set; }
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters!")]
        public string Name { get; set; }
        public List<Company> Companies { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<Department> Departments { get; set; }
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public Guid DepartmentId { get; set; }
        public List<Position> ReportingPositions { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public Guid ReportingPositionId { get; set; }
        public string ReportingPositionName { get; set; }

        public PositionModel()
        {
            _positionManagementService = new PositionManagementService();
            _departmentManagementService = new DepartmentManagementService();
            _companyManagementService = new CompanyManagementService();
            Companies = GetAllCompanies();
            ReportingPositions = GetAllPositions();
            Departments = GetAllDepartments();
        }
        public void PositionExcelFile(HttpPostedFileBase positionExcelFileBase)
        {

            string ExcuteMsg = string.Empty;
            int NumberOfColume = 0;
            try
            {
                //HttpPostedFileBase file = Request.Files["ProductExcel"];
                HttpPostedFileBase file = positionExcelFileBase;
                //Extaintion Check
                if (positionExcelFileBase.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
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
                        List<PositionModel> positionModelList = new List<PositionModel>();
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
                                    PositionModel positionModel = new PositionModel();
                                    positionModel.CompanyName =
                                        workSheet.Cells[rowIterator, 1].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    positionModel.DepartmentName = workSheet.Cells[rowIterator, 2].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    positionModel.Name = workSheet.Cells[rowIterator, 3].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 3].Value.ToString();
                                    positionModel.ReportingPositionName = workSheet.Cells[rowIterator, 4].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 4].Value.ToString();

                                    positionModelList.Add(positionModel);
                                }
                            }
                        }
                        //List data saving
                        if (positionModelList.Count > 0)
                        {

                            foreach (var item in positionModelList)
                            {
                                item.AddToPositionExcel();
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
        public void AddToPositionExcel()
        {
            _positionManagementService.AddToPositionExcel(CompanyName, DepartmentName, Name, ReportingPositionName);
        }
        private List<Department> GetAllDepartments()
        {
            return _departmentManagementService.GelAllDepartments();
        }

        public List<Position> GetAllPositions()
        {
            return _positionManagementService.GelAllPositions();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyManagementService.GetAllCompanies();
        }
          public PositionModel(Guid id) : this()
        {
            var position=_positionManagementService.GetPosition(id);

            this.Id = position.Id;
            this.Name = position.Name;
            if (position.Company != null)
            {
                this.CompanyId = position.Company.Id;
            }
            if (position.ReportingPosition != null)
            {
                this.ReportingPositionId = position.ReportingPosition.Id;
            }  
              if (position.Department != null)
            {
                this.DepartmentId = position.Department.Id;
            }
        }
          public void AddPosition()
          {
              _positionManagementService.AddPosition(Name, CompanyId, DepartmentId, ReportingPositionId);
          }
          public void EditPosition(Guid id)
          {
              _positionManagementService.EditPosition(id,Name, CompanyId, DepartmentId, ReportingPositionId);
          }
          internal void DeletePosition(Guid? id)
          {
              if (id.HasValue)
              {
                  _positionManagementService.DeletePosition(id.Value);
              }
              else
              {
                  throw new Exception();
              }

          }

          public Position LoadPositionData(Guid? id)
          {
              if (id.HasValue)
              {
                  return _positionManagementService.GetPosition(id.Value);
              }
              else
              {
                  throw new Exception();
              }

          }

    }
}