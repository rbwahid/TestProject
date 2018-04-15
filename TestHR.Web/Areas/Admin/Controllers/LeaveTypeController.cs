using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class LeaveTypeController : Controller
    {
        //
        // GET: /Admin/LeaveType/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
         
            return View();
        }
        [HttpPost]
        public ActionResult Add(LeaveTypeModel leaveTypeModel)
        {

            leaveTypeModel.AddLeaveType();
            TempData["message"] = "Successfully added Branch.";
            TempData["alertType"] = "success";
            return View(leaveTypeModel);
        }
	}
}