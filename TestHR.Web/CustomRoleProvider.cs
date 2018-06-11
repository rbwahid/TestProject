using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TestHR.AdminCenter;


namespace TestHR.Web
{
    #region role provider
    public class CustomRoleProvider:RoleProvider
    {
        private int _cacheTimeoutInMinute = 60;

        public override bool IsUserInRole(string userId, string roleName)
        {
            var userRoles = GetRolesForUser(userId);
            return userRoles.Contains(roleName);
        }


     
        public override string[] GetRolesForUser(string userid)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }
          
            string[] RoleTasks = new string[] { };
            Guid id = Guid.Parse(userid.Split('|')[0]);
            using (AdminCenterDbContext db = new AdminCenterDbContext())
            {
                RoleTasks = (from a in db.RoleTasks
                             join b in db.Employee on new {a.RoleId } equals new {b.RoleId }
                             where b.Id.Equals(id)
                             select a.Task).ToArray<string>();
               

            }
            return RoleTasks;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

    }
    #endregion
    #region role authorization
    // Roles authorization=========================================
    public class RolesAttribute : AuthorizeAttribute
    {
        // Multiple roles authorization=========================================

        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
        // Handle Unauthorized Request =========================================

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // The user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
            }else if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
            {
                // The user is not in any of the listed roles => 
                // show the unauthorized view
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/403_Unauthorized.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

    }
    #endregion


}
//=======================================================================================//
//Author : Md. Mahid Choudhury
//Creation Date : May 2018
//=======================================================================================//