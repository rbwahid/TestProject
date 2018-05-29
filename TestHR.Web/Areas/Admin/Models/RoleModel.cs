using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;
using TestHR.Web.Models;

namespace TestHR.Web.Areas.Admin.Models
{
    [NotMapped]
    public class RoleModel:Role
    {
        private RoleManagementService _roleManagementService { get; set; }

        public new List<RoleTaskCheckBoxModel> RoleTasks { get; set; }
        //public Guid? CompanyId { get; set; }

        //public List<EmployeeEducationHistory> EducationHistories { get; set; }


        public RoleModel()
        {
          
            _roleManagementService=new RoleManagementService();
            RoleTasks = RoleTaskCheckBoxModel.TaskNames.OrderBy(x=>x.TaskName).ToList();
           

        }
        public List<Role> GetAllRoles()
        {
            return _roleManagementService.GetAllRoles();
        }
        public void AddRole()
        {
            _roleManagementService.AddRole(RoleName, RoleTasks);
        }

    }
    
   
}