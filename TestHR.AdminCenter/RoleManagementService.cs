using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;


namespace TestHR.AdminCenter
{
    public class RoleManagementService
    {
        private AdminCenterDbContext _context;
        private RoleUnitOfWork _roleUnitOfWork;
        private RoleTaskCheckBoxModel _roleTaskCheckBoxModel;

        public RoleManagementService()
        {
            _context = new AdminCenterDbContext();
            _roleUnitOfWork = new RoleUnitOfWork(_context);


        }

        public List<Role> GetAllRoles()
        {
            return _roleUnitOfWork.RoleRepository.GetAll().ToList();
        }

        public Role GetRole(Guid id)
        {
            return _roleUnitOfWork.RoleRepository.GetById(id);
        }

        public void DeleteRole(Guid id)
        {
            _roleUnitOfWork.RoleRepository.DeleteById(id);
            _roleUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _roleUnitOfWork.RoleRepository.GetAll().Count();

        }
        public void AddRole(string roleName, List<RoleTaskCheckBoxModel> roleTaskList)
        {
            
            var role = new Role()
            {
                RoleName = roleName,
                Status = 1
            };
            List<RoleTask> taskList= new List<RoleTask>();
            foreach (var taskItem in roleTaskList.Where(x=>x.IsChecked))
            {
                var task = new RoleTask{
                    Task=taskItem.TaskName
                    };
                taskList.Add(task);
            }
            role.RoleTasks = taskList;
            _roleUnitOfWork.RoleRepository.Add(role);
            _roleUnitOfWork.Save();
           
        }
        public void EditRole(Guid id, string roleName, List<RoleTaskCheckBoxModel> roleTaskList)
        {
            var role = _roleUnitOfWork.RoleRepository.GetById(id);
            role.RoleName = roleName;
            var roleTasks = role.RoleTasks.ToList();
            foreach (var removeTask in roleTasks)
            {
                _context.RoleTasks.Remove(removeTask);
            }
            _roleUnitOfWork.Save();

           var taskList = roleTaskList.Where(x => x.IsChecked).Select(taskItem => new RoleTask
           {
               Task = taskItem.TaskName
           }).ToList();
            role.RoleTasks = taskList;
            
            _roleUnitOfWork.Save();
            
        }
    }
    public class RoleTaskCheckBoxModel
    {

        public string TaskName { get; set; }
        public string TaskNameDisplay { get; set; }
        public bool IsChecked { get; set; }


        public static List<RoleTaskCheckBoxModel> TaskNames = new List<RoleTaskCheckBoxModel>
        {
             new RoleTaskCheckBoxModel {TaskName="Employee", TaskNameDisplay="Employee"},
            new RoleTaskCheckBoxModel {TaskName="Employee_Add", TaskNameDisplay="Employee Add"},
            new RoleTaskCheckBoxModel {TaskName="Employee_Edit", TaskNameDisplay="Employee Edit"},
            new RoleTaskCheckBoxModel {TaskName="Branch_Configuration", TaskNameDisplay="Branch Congiguration"},
            new RoleTaskCheckBoxModel {TaskName="Department_Configuration", TaskNameDisplay="Department Configuration"},
            new RoleTaskCheckBoxModel {TaskName="Holiday_Configuration", TaskNameDisplay="Holiday Configuration"},
            new RoleTaskCheckBoxModel {TaskName="Shift_Configuration", TaskNameDisplay="Shift Configuration"},
            new RoleTaskCheckBoxModel {TaskName="LeaveType_Configuration", TaskNameDisplay="LeaveType Configuration"},
            new RoleTaskCheckBoxModel {TaskName="Position_Configuration", TaskNameDisplay="Position Configuration"},
            new RoleTaskCheckBoxModel {TaskName="Role_Configuration", TaskNameDisplay="Role Configuration"},
            new RoleTaskCheckBoxModel {TaskName="Company_Configuration", TaskNameDisplay="Company Configuration"},
          
        };


    }
   
}
