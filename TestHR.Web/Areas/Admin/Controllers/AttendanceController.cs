using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class AttendanceController : Controller
    {
        //
        // GET: /Admin/Attendance/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttendanceLogFileImport()
        {
            return View("AttendanceLogFileImport");
        }
        [HttpPost]
        public ActionResult AttendanceLogFileImport(HttpPostedFileBase attendanceLogExcelFile)
        {
            if (ModelState.IsValid)
            {
                AttendanceLogModel attendanceLogModel=new AttendanceLogModel();
                attendanceLogModel.AttendanceLogFileImport(attendanceLogExcelFile);
            }
            return RedirectToAction("AttendanceLogViewTable");
        }

        public ActionResult AttendanceLogViewTable()
        {
            var attendanceLogs = new Models.AttendanceLogModel().GetAllaAttendanceLogs();
            return View(attendanceLogs);
        }
	}
}