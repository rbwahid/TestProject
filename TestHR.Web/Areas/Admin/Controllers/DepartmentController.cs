﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Admin/Department/
        public ActionResult Index()
        {
            var departments = new Models.DepartmentModel().GetAllDepartments();
            return View(departments);
        }
        public ActionResult Add()
        {
            var departmentModel = new DepartmentModel();
            return View(departmentModel);
        }
        [HttpPost]
        public ActionResult Add(DepartmentModel departmentModel)
        {
            
            departmentModel.AddDepartment();
            TempData["message"] = "Successfully added Branch.";
            TempData["alertType"] = "success";
           

            //catch (Exception e)
            //{
            //    TempData["message"] = "Failed to Add Branch.";
            //    TempData["alertType"] = "danger";
            //    Console.Write(e.Message);
            //}

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
            model.EditDepartment(model.Id);
            return RedirectToAction("Index");
        }
	}
}