using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class HolidayController : Controller
    {
        //
        // GET: /Admin/Holiday/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            var holidayModel = new HolidayModel();
            return View(holidayModel);
        }
        [HttpPost]
        public ActionResult Add(HolidayModel holidayModel)
        {
            //try
            //{
            holidayModel.AddHoliday();
            TempData["message"] = "Successfully added Branch.";
            TempData["alertType"] = "success";
            //}

            //catch (Exception e)
            //{
            //    TempData["message"] = "Failed to Add Branch.";
            //    TempData["alertType"] = "danger";
            //    Console.Write(e.Message);
            //}

            return View(holidayModel);
        }
	}
}