using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class CalendarController : Controller
    {
        //
        // GET: /Admin/Calender/
        public ActionResult Index(CalendarModel model)
        {
            return View(model.GetAllHolidays());
        }

        public ActionResult CalendarImport()
        {
            return View("CalendarImport");
        }
        [HttpPost]
        public ActionResult CalendarImport(HttpPostedFileBase holidayExcelFile)
        {
            if (ModelState.IsValid)
            {
                string ExcuteMsg = string.Empty;
                int NumberOfColume = 0;
                try
                {
                    //HttpPostedFileBase file = Request.Files["ProductExcel"];
                    HttpPostedFileBase file = holidayExcelFile;
                    //Extaintion Check
                    if (holidayExcelFile.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
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
                            List<CalendarModel> holidayList = new List<CalendarModel>();
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    if (workSheet.Cells[rowIterator, 2].Value != null && workSheet.Cells[rowIterator, 3].Value!=null) 
                                    { 
                                        CalendarModel calenderModel = new CalendarModel
                                        {
                                            DateFrom = Convert.ToDateTime(workSheet.Cells[rowIterator, 2].Value),
                                            DateTo = Convert.ToDateTime(workSheet.Cells[rowIterator, 3].Value),
                                            Name = workSheet.Cells[rowIterator, 4].Value == null
                                                ? null
                                                : workSheet.Cells[rowIterator, 4].Value.ToString(),
                                            Description = workSheet.Cells[rowIterator, 5].Value == null
                                            ? null
                                            : workSheet.Cells[rowIterator, 5].Value.ToString(),

                                        };
                                        holidayList.Add(calenderModel);
                                    }
                                }
                            }
                            
                            //List data saving
                            if (holidayList.Count > 0)
                            {
                                string fmt = "000000000.##";
                                int lastInvoiceId = 0;
                                foreach (var item in holidayList)
                                {
                                    item.AddToCalendar();
                                }
                            }



                        }
                    }
                    else
                    {
                        ViewBag.Error = "File type is incorrect <br>";
                    }
                }
                catch (Exception r)
                {
                    ExcuteMsg = r.Message;
                }
                ViewBag.ExcuteMsg = ExcuteMsg;
            }
            return View();
        }
	}
}