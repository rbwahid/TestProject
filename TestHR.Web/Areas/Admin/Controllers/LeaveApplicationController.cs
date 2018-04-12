using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class LeaveApplicationController : Controller
    {
        //
        // GET: /Admin/LeaveApplication/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            LeaveApplicationModel model = new LeaveApplicationModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult Add(LeaveApplicationModel leaveApplicationModel)
        {

            leaveApplicationModel.AddLeaveApplication();
            TempData["message"] = "Successfully added Branch.";
            TempData["alertType"] = "success";
            return View(leaveApplicationModel);
        }
	}
}