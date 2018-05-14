using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class LeaveApplicationController : Controller
    {
        //
        // GET: /Admin/LeaveApplication/
        public ActionResult Index()
        {
            var leaveApplications = new LeaveApplicationModel().GetLeaveApplications();
            return View(leaveApplications);
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var leaveApplicationModel = new LeaveApplicationModel(id);
            return View(leaveApplicationModel);
        }

        [HttpPost]
        public ActionResult Edit(LeaveApplicationModel model)
        {
            model.EditLeaveApplication();
            return RedirectToAction("Index");
        }

        public ActionResult GetLeaveInfoById(Guid id)
        {
            var leaveApplication = new LeaveApplicationModel().LoadLeaveApplication(id);
            return Json(leaveApplication, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LeaveApprove(Guid Id, int Status,string Comments)
        {
            LeaveApplicationModel leaveApplication = new LeaveApplicationModel(Id);
            leaveApplication.Status = Status;
            leaveApplication.Comments = Comments;
            leaveApplication.LeaveApprove();
            return RedirectToAction("Index");
        }

        public ActionResult LeaveReport()
        {
            var leaveReport = new LeaveApplicationModel().LeaveReport();
            return View(leaveReport);
        }

	}
}