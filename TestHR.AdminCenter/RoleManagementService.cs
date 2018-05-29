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
        //private CompanyUnitOfWork _companyUnitOfWork;
        //private DepartmentUnitOfWork _departmentUnitOfWork;
        //private BranchUnitOfWork _branchUnitOfWork;
        //private PositionUnitOfWork _positionUnitOfWork;
        //private EducationHistoryUnitOfWork _educationHistoryUnitOfWork;
        //private CareerHistoryUnitOfWork _careerHistoryUnitOfWork;
        public RoleManagementService()
        {
            _context = new AdminCenterDbContext();
            _roleUnitOfWork = new RoleUnitOfWork(_context);
            //_companyUnitOfWork = new CompanyUnitOfWork(_context);
            //_departmentUnitOfWork = new DepartmentUnitOfWork(_context);
            //_positionUnitOfWork = new PositionUnitOfWork(_context);
            //_branchUnitOfWork = new BranchUnitOfWork(_context);
            //_educationHistoryUnitOfWork = new EducationHistoryUnitOfWork(_context);
            //_careerHistoryUnitOfWork = new CareerHistoryUnitOfWork(_context);

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
                RoleName = roleName
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
    //    public void EditEmployee(Guid id, string firstName, string middleName, string lastName, string fathersName, string motherName,
    //         string souseName, string phoneNumber, string presentAddress, string pernamentAddress, string email, string religion, string nationality,
    //         string nId, string passportNo, Guid? companyId, Guid? branchId, Guid? departmentId, Guid? positionId, Guid? reportingToId, List<EmployeeEducationHistory> educationHistories, List<EmployeeCareerHistory> careerHistories)
    //    {
    //        var employee = _employeeUnitOfWork.EmployeeRepository.GetById(id);
    //        employee.FirstName = firstName;
    //        employee.MiddleName = middleName;
    //        employee.LastName = lastName;
    //        employee.FathersName = fathersName;
    //        employee.MothersName = motherName;
    //        employee.SouseName = souseName;
    //        employee.PhoneNumber = phoneNumber;
    //        employee.PresentAddress = presentAddress;
    //        employee.PernamentAddress = pernamentAddress;
    //        employee.Email = email;
    //        employee.Religion = religion;
    //        employee.Nationality = nationality;
    //        employee.Nid = nId;
    //        employee.PassportNo = passportNo;
    //        employee.ReportingTo = _employeeUnitOfWork.EmployeeRepository.GetById(reportingToId.Value);
    //        employee.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId.Value);
    //        employee.Branch = _branchUnitOfWork.BranchRepository.GetById(branchId.Value);
    //        employee.Department = _departmentUnitOfWork.DepartmentRepository.GetById(departmentId.Value);
    //        employee.Position = _positionUnitOfWork.PositionRepository.GetById(positionId.Value);
    //        employee.EmployeeCareerHistory.Clear();
    //        employee.EmployeeCareerHistory.Clear();
    //        foreach (var eduItem in educationHistories)
    //        {
    //            if (eduItem.Id == Guid.Empty)
    //            {
    //                eduItem.Id = Guid.NewGuid();
    //                employee.EmployeeEducationHistory.Add(eduItem);
    //            }
    //            else
    //            {
    //                var educationHistory = _educationHistoryUnitOfWork.EducationHistoryRepository.GetById(eduItem.Id);
    //                educationHistory.Degree = eduItem.Degree;
    //                educationHistory.InstituteName = eduItem.InstituteName;
    //                educationHistory.PassingYear = eduItem.PassingYear;
    //                educationHistory.Result = eduItem.Result;
    //                educationHistory.Subject = eduItem.Subject;
    //                _educationHistoryUnitOfWork.Save();
    //            }
    //        }

    //        foreach (var careerHistory in careerHistories)
    //        {
    //            if (careerHistory.Id == Guid.Empty)
    //            {
    //                careerHistory.Id = Guid.NewGuid();
    //                employee.EmployeeCareerHistory.Add(careerHistory);
    //            }
    //            else
    //            {
    //                var employeeCareerHistory =
    //                    _careerHistoryUnitOfWork.EducationHistoryRepository.GetById(careerHistory.Id);
    //                employeeCareerHistory.CompanyName = careerHistory.CompanyName;
    //                employeeCareerHistory.CompanyAddress = careerHistory.CompanyAddress;
    //                employeeCareerHistory.Department = careerHistory.Department;
    //                employeeCareerHistory.Position = careerHistory.Position;
    //                employeeCareerHistory.StartDate = careerHistory.StartDate;
    //                employeeCareerHistory.EndDate = careerHistory.EndDate;
    //                _careerHistoryUnitOfWork.Save();

    //            }
    //        }
          

    //        _employeeUnitOfWork.Save();
    //    }
    }
    public class RoleTaskCheckBoxModel
    {

        public string TaskName { get; set; }
        public string TaskNameDisplay { get; set; }
        public bool IsChecked { get; set; }


        public static List<RoleTaskCheckBoxModel> TaskNames = new List<RoleTaskCheckBoxModel>
        {
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
