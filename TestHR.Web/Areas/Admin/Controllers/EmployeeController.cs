﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;

namespace TestHR.Web.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Admin/Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            EmployeeModel employeeModel=new EmployeeModel();
            return View(employeeModel);
        }
        [HttpPost]
        public ActionResult Add(EmployeeModel employeeModel)
        {

            employeeModel.AddEmployee();
            TempData["message"] = "Successfully added Branch.";
            TempData["alertType"] = "success";



            return View(employeeModel);
        }
	}
}