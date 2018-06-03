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
         public RoleModel(Guid id) : this()
        {
            var role=_roleManagementService.GetRole(id);

            this.Id = role.Id;
            this.RoleName = role.RoleName;
             this.Status = role.Status;

             this.RoleTasks = new List<RoleTaskCheckBoxModel>();
             foreach (var item in RoleTaskCheckBoxModel.TaskNames.OrderBy(x => x.TaskNameDisplay))
             {
                 item.IsChecked = role.RoleTasks.Any(x => x.Task.Equals(item.TaskName));
                 this.RoleTasks.Add(item);
             }
        }
        public void AddRole()
        {
            _roleManagementService.AddRole(RoleName, RoleTasks);
        }
        public void EditRole(Guid id)
        {
            _roleManagementService.EditRole(id, RoleName, RoleTasks);
        }
    }
    
   
}