﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
     [Authorize]
    public class DepartmentController : Controller
    {
    [Roles("Global_SupAdmin,Department_Configuration")]
        public ActionResult Index()
        {
            var departments = new Models.DepartmentModel().GetAllDepartments().Where(x=>x.IsDelete==false);
            return View(departments);
        }
         [Roles("Global_SupAdmin,Department_Configuration")]
        public ActionResult Add()
        {
            var departmentModel = new DepartmentModel();
            return View(departmentModel);
        }
        [HttpPost]
        public ActionResult Add(DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                departmentModel.AddDepartment();
                TempData["message"] = "Successfully added Department.";
                TempData["alertType"] = "success";
                return RedirectToAction("Index");
            }
            return View(departmentModel);
        }
        public ActionResult Edit(Guid id)
        {
            DepartmentModel department = new DepartmentModel(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        [HttpPost]
        public ActionResult Edit(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditDepartment(model.Id);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DepartmentFileImport()
        {
            return View("DepartmentFileImport");
        }
        [HttpPost]
        public ActionResult DepartmentFileImport(HttpPostedFileBase departmentExcelFile)
        {
            if (ModelState.IsValid)
            {
                DepartmentModel departmentModel = new DepartmentModel();
                departmentModel.DepartmentFileImport(departmentExcelFile);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid? id)
        {
            try
            {
                new DepartmentModel().DeleteDepartment(id);
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