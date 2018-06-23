using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class ShiftController : Controller
    {
        //
        // GET: /Admin/Shift/
        public ActionResult Index()
        {
            var shifts = new Models.ShiftModel().GetAllShift().Where(x=>x.IsDelete==false);
            return View(shifts);
        }

        public ActionResult Add()
        {
            var shiftModel = new ShiftModel();
            return View(shiftModel);
        }

        [HttpPost]
        public ActionResult Add(ShiftModel shiftModel)
        {
            try
            {
                shiftModel.AddShift();
                TempData["message"] = "Successfully added Branch.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Shift.";
                TempData["alertType"] = "danger";
                Console.Write(e.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid? id)
        {
            try
            {
                new ShiftModel().DeleteShift(id);
                TempData["message"] = "Successfully Deleted Company.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Company.";
                TempData["alertType"] = "danger";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var model = new ShiftModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ShiftModel model)
        {
            model.EditShift(model.Id);
            return RedirectToAction("Index");
        }
	}
}