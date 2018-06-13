using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class LeaveTypeController : Controller
    {
        //
        // GET: /Admin/LeaveType/
        public ActionResult Index()
        {
            var leaveType = new LeaveTypeModel().GetAllLeaveTypes().Where(e=>e.IsDelete==false);
            return View(leaveType);
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
            return RedirectToAction("Index");
        }
        public ActionResult Edit(Guid id)
        {
            var leaveType = new LeaveTypeModel(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }
        [HttpPost]
        public ActionResult Edit(LeaveTypeModel model)
        {
            model.EditLeaveType();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid? id)
        {
            try
            {
                new LeaveTypeModel().DeleteLeave(id);
                TempData["message"] = "Successfully Deletd LeaveType.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add LeaveType.";
                TempData["alertType"] = "danger";
            }

            return RedirectToAction("Index");
        }
	}
}