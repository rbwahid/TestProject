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
           
            return  View();
        }

       
	}
}