using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;
using ModelState = System.Web.ModelBinding.ModelState;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class BranchController : Controller
    {
        //
        // GET: /Admin/Branch/
        public ActionResult Index()
        {
            var branches = new Models.BranchModel().GetAllBranches().Where(x=>x.IsDelete==false);
            return View(branches);
        }

        //=== Branch Excel File Import ===//
        public ActionResult BranchExcelFileImport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BranchExcelFileImport(HttpPostedFileBase branchExcelFileBase)
        {
            if (ModelState.IsValid)
            {
                BranchModel branchModel = new BranchModel();
                branchModel.BranchExcelFile(branchExcelFileBase);
            }
            return RedirectToAction("Index");
        }
        //public ActionResult BranchExcelFileDataList()
        //{
        //    var branchModel = new BranchModel().GetAllBranches();
        //    return View(branchModel);
        //}
        //=== End Branch Excel Import ===//
        public ActionResult Add()
        {
            var branchModel = new BranchModel();
            return View(branchModel);
        }
        [HttpPost]
        public ActionResult Add(BranchModel branchModel)
        {
            //try
            //{
                branchModel.AddBranch();
                TempData["message"] = "Successfully added Branch.";
                TempData["alertType"] = "success";
            //}

            //catch (Exception e)
            //{
            //    TempData["message"] = "Failed to Add Branch.";
            //    TempData["alertType"] = "danger";
            //    Console.Write(e.Message);
            //}

            return View(branchModel);
        }

        public ActionResult Edit(Guid id)
        {
            BranchModel branch=new BranchModel(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }
        [HttpPost]
        public ActionResult Edit(BranchModel model)
        {
            model.EditBranch(model.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid? id)
        {
            try
            {
                new BranchModel().DeleteBranch(id);
                TempData["message"] = "Successfully Deletd Company.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Company.";
                TempData["alertType"] = "danger";
            }

            return RedirectToAction("Index");
        }
	}
}