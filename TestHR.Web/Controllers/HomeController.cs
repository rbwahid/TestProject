using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHR.Entities;
using TestHR.AdminCenter;

namespace TestHR.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var company = new Company();
            var context = new AdminCenterDbContext();
            var repo = new CompanyRepository(context);
            repo.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}