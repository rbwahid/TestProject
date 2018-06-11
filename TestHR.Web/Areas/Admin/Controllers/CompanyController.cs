using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Admin/Company

        public ActionResult Index()
        {
            var companies = new Models.CompanyModel().GetAllCompanies().ToList();
            return View(companies);
        }

        public ActionResult Add()
        {
            var companyModel = new CompanyModel();
            return View(companyModel);
        }

        [HttpPost]
        public ActionResult Add(CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    companyModel.AddCompany();
                    TempData["message"] = "Successfully added Company.";
                    TempData["alertType"] = "success";
                //}

                //catch (Exception e)
                //{
                //    TempData["message"] = "Failed to Add Company.";
                //    TempData["alertType"] = "danger";
                //    Console.Write(e.Message);
                //}
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(Guid id)
        {
            CompanyModel company = new CompanyModel(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }
        [HttpPost]
        public ActionResult Edit(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditCompany(model.Id);
            }
            return RedirectToAction("Index");
        }
        public ActionResult CompanyFileImport()
        {
            return View("CompanyFileImport");
        }
        [HttpPost]
        public ActionResult CompanyFileImport(HttpPostedFileBase companyExcelFile)
        {
            if (ModelState.IsValid)
            {
                CompanyModel companyModel = new CompanyModel();
                companyModel.CompanyFileImport(companyExcelFile);
            }
            return RedirectToAction("CompanyFileImport");
        }
        public ActionResult Delete(Guid? id)
        {
            try
            {
                new CompanyModel().DeleteCompany(id);
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