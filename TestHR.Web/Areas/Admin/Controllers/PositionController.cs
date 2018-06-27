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
            var position = new Models.PositionModel().GetAllPositions().Where(x=>x.IsDelete==false);
            return View(position);
        }
        //=== Position Excel File Import ===//
        public ActionResult PositionExcelFileImport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PositionExcelFileImport(HttpPostedFileBase positionExcelFileBase)
        {
            if (ModelState.IsValid)
            {
                PositionModel positionModel = new PositionModel();
                positionModel.PositionExcelFile(positionExcelFileBase);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Add()
        {
            var positionModel = new PositionModel();
            return View(positionModel);
        }

        [HttpPost]
        public ActionResult Add(PositionModel positionModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    positionModel.AddPosition();
                    TempData["message"] = "Successfully added Position.";
                    TempData["alertType"] = "success";
                    return RedirectToAction("Index");
                }

                catch (Exception e)
                {
                    TempData["message"] = "Failed to Add Company.";
                    TempData["alertType"] = "danger";
                    Console.Write(e.Message);
                }

            }
            return View(positionModel);
        }

        public ActionResult Edit(Guid id)
        {
            PositionModel position = new PositionModel(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        [HttpPost]
        public ActionResult Edit(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditPosition(model.Id);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        public ActionResult Delete(Guid? id)
        {
            try
            {
                new PositionModel().DeletePosition(id);
                TempData["message"] = "Successfully Deleted Position.";
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