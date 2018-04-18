using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class BranchController : Controller
    {
        //
        // GET: /Admin/Branch/
        public ActionResult Index()
        {
            var branches = new Models.BranchModel().GetAllBranches();
            return View(branches);
        }

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
	}
}