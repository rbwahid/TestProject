using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TestHR.Web.Areas.Admin.Models;
using TestHR.Web.Controllers;

namespace TestHR.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        [Roles("Global_SupAdmin,Employee_Add,Employee_Edit")]
        public ActionResult Index()
        {
            var employees = new Models.EmployeeModel().GetAllEmployee().Where(x=>x.IsDelete==false);
            return View(employees);
        }

        public ActionResult Add()
        {
            var employeeModel = new EmployeeModel();
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult Add(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                MD5 md5Hash = MD5.Create();
                string hashPassword = UserController.GetMd5Hash(md5Hash, employeeModel.Password);
                employeeModel.Password = hashPassword;
                employeeModel.AddEmployee();
                TempData["message"] = "Successfully added Branch.";
                TempData["alertType"] = "success";
                return RedirectToAction("/Admin/Employee/Index");
            }
            return View(employeeModel);

        }

        public ActionResult Edit(Guid id)
        {
            var employee = new EmployeeModel(id);

            if (employee==null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Delete(Guid? id)
        {
            try
            {
                new EmployeeModel().DeleteEmployee(id);
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

        public ActionResult Edit(EmployeeModel model)
        {
            model.EditEmployee();            
			return RedirectToAction("Index");
        }
    }
}