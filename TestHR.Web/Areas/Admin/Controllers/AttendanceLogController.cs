using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class AttendanceLogController : Controller
    {
        //
        // GET: /Admin/AttendanceLog/
        public ActionResult Index()
        {
            var attendanceLogModel = new AttendanceLogModel().GetAllaAttendanceLogs();
            return View(attendanceLogModel);
        }

        public ActionResult Pool()
        {
            var attendanceLog = new AttendanceLogModel();
            attendanceLog.PoolLog();
            return RedirectToAction("Index");
        }
	}
}