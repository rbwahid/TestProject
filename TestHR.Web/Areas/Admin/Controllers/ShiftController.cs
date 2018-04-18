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
            return View();
        }
        public ActionResult Add()
        {
            var shiftModel = new ShiftModel().GetDefaultShift();
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

            return View(shiftModel);
        }
	}
}