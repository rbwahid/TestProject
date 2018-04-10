using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class PositionController : Controller
    {

        public ActionResult Index()
        {
            var positionModel = new PositionModel();
            return View(positionModel);
        }

        public ActionResult Add()
        {
            var positionModel = new PositionModel();
            return View(positionModel);
        }

        [HttpPost]
        public ActionResult Add(PositionModel positionModel)
        {
            try
            {
                positionModel.AddPosition();
                TempData["message"] = "Successfully added Company.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Company.";
                TempData["alertType"] = "danger";
                Console.Write(e.Message);
            }

            return View(positionModel);
        }

        public ActionResult Delete(Guid? id)
        {
            try
            {
                new PositionModel().DeletePosition(id);
                TempData["message"] = "Successfully Deletd Position.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Position.";
                TempData["alertType"] = "danger";
            }

            return RedirectToAction("Index");
        }
	}
}