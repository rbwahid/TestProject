using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using TestHR.AdminCenter;
using TestHR.Entities;
using TestHR.Web.Areas.Admin.Models;
using TestHR.Web.Models;
using LoginViewModel = TestHR.Web.Areas.Admin.Models.LoginViewModel;
namespace TestHR.Web.Controllers
{

    public class UserController : Controller
    {
        private AdminCenterDbContext db = new AdminCenterDbContext();
        public UserManager<ApplicationUser> UserManager { get; private set; }
        #region login logout area
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {

                // find user by username first
                var user = db.Employee.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower() && !u.IsDelete);
                if (user != null)
                {
                    MD5 md5Hash = MD5.Create();
                    string hashPassword = GetMd5Hash(md5Hash, model.Password);
                    var validCredentials = db.Employee.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower() && u.Password.Equals(hashPassword) && !u.IsDelete);
                    if (validCredentials == null)
                    {
                        ModelState.AddModelError("", "Invalid credentials. Please try again.");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Id + "|" + user.UserName.ToLower(), model.RememberMe);
                        Session["sessionid"] = System.Web.HttpContext.Current.Session.SessionID;
                        SaveLogin(model.UserName.ToLower(), System.Web.HttpContext.Current.Session.SessionID,true);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                ModelState.Remove("Password");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return  RedirectToAction("Login", "User");
        }
        #endregion
        public ActionResult Roles()
        {
            var roles = new RoleModel().GetAllRoles().Where(x => x.IsDelete == false);
            return View("RoleList",roles);
        }
       [Roles("Global_SupAdmin,Role_Configuration")]
        public ActionResult AddRole()
        {
            var roleModel = new RoleModel();
            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(RoleModel roleModel)
        {
            try
            {
                roleModel.AddRole();
                TempData["message"] = "Successfully added Role.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Role.";
                TempData["alertType"] = "danger";
                Console.Write(e.Message);
            }

            return RedirectToAction("Roles");
        }


        #region helper modules
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void SaveLogin(string userId, string sessionId, bool loggedIn)
        {
            Logins login = new Logins
            {
                UserId = userId,
                SessionId = sessionId,
                LoggedIn = loggedIn,
                LoggedInDateTime = DateTime.Now
            };
            db.Logins.Add(login);
            db.SaveChanges();
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}