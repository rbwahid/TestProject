using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Services.Description;
using OfficeOpenXml;
using TestHR.Entities;
using TestHR.AdminCenter;

namespace TestHR.Web.Areas.Admin.Models
{
    public class AttendanceLogModel
    {
        private AttendanceLogManagementService _attendanceLogManagementService { get; set; }

        public string Name { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan PunchTime { get; set; }
        public AttendanceLogModel()
        {
            _attendanceLogManagementService = new AttendanceLogManagementService();
        }
        public List<AttendanceLog> GetAllaAttendanceLogs()
        {

            return _attendanceLogManagementService.GetAllAttendanceLog();
        }

       
        public void AddToAttendanceLog()
        {
            _attendanceLogManagementService.AddToAttendanceLog(Name, AttendanceDate,PunchTime);
        }

        public void AttendanceLogFileImport(HttpPostedFileBase attendanceLogExcelFile)
        {
            
            string ExcuteMsg = string.Empty;
            int NumberOfColume = 0;
            try
            {
                //HttpPostedFileBase file = Request.Files["ProductExcel"];
                HttpPostedFileBase file = attendanceLogExcelFile;
                //Extaintion Check
                if (attendanceLogExcelFile.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
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
                        List<AttendanceLogModel> attendanceLogList = new List<AttendanceLogModel>();
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
                                    AttendanceLogModel attendanceLogModel = new AttendanceLogModel();
                                    attendanceLogModel.Name =
                                        workSheet.Cells[rowIterator, 1].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    attendanceLogModel.AttendanceDate = Convert.ToDateTime(workSheet.Cells[rowIterator, 2].Value);
                                    DateTime dt= Convert.ToDateTime(workSheet.Cells[rowIterator, 3].Value);
                                    attendanceLogModel.PunchTime = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                                    //string date = String.Format("{0:dd-MMM-yy}",workSheet.Cells[rowIterator, 2].Value);
                                    //string dt = string.Format("{0:hh:mm:ss tt}", workSheet.Cells[rowIterator, 3].Value);
                                    //date = date + " " + dt;
                                    //attendanceLogModel.AttendanceDateTime = Convert.ToDateTime(date);
                                    attendanceLogList.Add(attendanceLogModel);
                                }
                            }
                        }
                        //List data saving
                        if (attendanceLogList.Count > 0)
                        {
                            string fmt = "000000000.##";
                            int lastInvoiceId = 0;
                            foreach (var item in attendanceLogList)
                            {
                                item.AddToAttendanceLog();
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

        
        internal void DeleteAttendanceLog(Guid? id)
        {
            if (id.HasValue)
            {
                _attendanceLogManagementService.DeleteAttendanceLog(id.Value);
            }                
            else
            {
                throw new Exception();
            }
        }

        public AttendanceLog LoadAttendanceLog(Guid? id)
        {
            if (id.HasValue)
            {
                return _attendanceLogManagementService.GetAttendanceLog(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
}