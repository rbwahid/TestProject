using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class EmployeeManagementService
    {
        private AdminCenterDbContext _context;
        private EmployeeUnitOfWork _employeeUnitOfWork;
        private CompanyUnitOfWork _companyUnitOfWork;
        private DepartmentUnitOfWork _departmentUnitOfWork;
        private BranchUnitOfWork _branchUnitOfWork;
        private PositionUnitOfWork _positionUnitOfWork;
        private EducationHistoryUnitOfWork _educationHistoryUnitOfWork;
        private CareerHistoryUnitOfWork _careerHistoryUnitOfWork;
        public EmployeeManagementService()
        {
            _context = new AdminCenterDbContext();
            _employeeUnitOfWork = new EmployeeUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
            _departmentUnitOfWork = new DepartmentUnitOfWork(_context);
            _positionUnitOfWork = new PositionUnitOfWork(_context);
            _branchUnitOfWork = new BranchUnitOfWork(_context);
            _educationHistoryUnitOfWork = new EducationHistoryUnitOfWork(_context);
            _careerHistoryUnitOfWork = new CareerHistoryUnitOfWork(_context);

        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeUnitOfWork.EmployeeRepository.GetAll().ToList();
        }

        public Employee GetEmployee(Guid id)
        {
            return _employeeUnitOfWork.EmployeeRepository.GetById(id);
        }

        public void DeleteEmployee(Guid id)
        {
            _employeeUnitOfWork.EmployeeRepository.DeleteById(id);
            _employeeUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _employeeUnitOfWork.EmployeeRepository.GetAll().Count();

        }
        public void AddEmployee(string firstName, string middleName, string lastName, string fathersName, string motherName,
            string souseName, string phoneNumber, string presentAddress, string pernamentAddress, string email, string religion, string nationality,
            string nId, string passportNo, Guid? companyId, Guid? branchId, Guid? departmentId, Guid? positionId,Guid? reportingToId,int? fpId,int? cardNo, List<EmployeeEducationHistory> educationHistories, List<EmployeeCareerHistory> careerHistories)
        {
            var employee = new Employee();
            employee.FirstName = firstName;
            employee.MiddleName = middleName;
            employee.LastName = lastName;
            employee.FathersName = fathersName;
            employee.MothersName = motherName;
            employee.SouseName = souseName;
            employee.PhoneNumber = phoneNumber;
            employee.PresentAddress = presentAddress;
            employee.PernamentAddress = pernamentAddress;
            employee.Email = email;
            employee.Religion = religion;
            employee.Nationality = nationality;
            employee.Nid = nId;
            employee.PassportNo = passportNo;
            if (reportingToId.HasValue)
            {
                employee.ReportingTo = _employeeUnitOfWork.EmployeeRepository.GetById(reportingToId.Value);
            }
            employee.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId.Value);
            employee.Branch = _branchUnitOfWork.BranchRepository.GetById(branchId.Value);
            employee.Department = _departmentUnitOfWork.DepartmentRepository.GetById(departmentId.Value);
            employee.Position = _positionUnitOfWork.PositionRepository.GetById(positionId.Value);
            employee.FPId = fpId;
            employee.CardNo = cardNo;
            employee.EmployeeCareerHistory = careerHistories;
            employee.EmployeeEducationHistory = educationHistories;
            _employeeUnitOfWork.EmployeeRepository.Add(employee);
            _employeeUnitOfWork.Save();
        }
        public void EditEmployee(Guid id, string firstName, string middleName, string lastName, string fathersName, string motherName,
             string souseName, string phoneNumber, string presentAddress, string pernamentAddress, string email, string religion, string nationality,
             string nId, string passportNo, Guid? companyId, Guid? branchId, Guid? departmentId, Guid? positionId, Guid? reportingToId,int? fpId,int? cardNo, List<EmployeeEducationHistory> educationHistories, List<EmployeeCareerHistory> careerHistories)
        {
            var employee = _employeeUnitOfWork.EmployeeRepository.GetById(id);
            employee.FirstName = firstName;
            employee.MiddleName = middleName;
            employee.LastName = lastName;
            employee.FathersName = fathersName;
            employee.MothersName = motherName;
            employee.SouseName = souseName;
            employee.PhoneNumber = phoneNumber;
            employee.PresentAddress = presentAddress;
            employee.PernamentAddress = pernamentAddress;
            employee.Email = email;
            employee.Religion = religion;
            employee.Nationality = nationality;
            employee.Nid = nId;
            employee.PassportNo = passportNo;
            if (reportingToId.HasValue)
            {
                employee.ReportingTo = _employeeUnitOfWork.EmployeeRepository.GetById(reportingToId.Value);
            }
            employee.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId.Value);
            employee.Branch = _branchUnitOfWork.BranchRepository.GetById(branchId.Value);
            employee.Department = _departmentUnitOfWork.DepartmentRepository.GetById(departmentId.Value);
            employee.Position = _positionUnitOfWork.PositionRepository.GetById(positionId.Value);
            employee.FPId = fpId;
            employee.CardNo = cardNo;
            employee.EmployeeCareerHistory.Clear();
            employee.EmployeeCareerHistory.Clear();
            foreach (var eduItem in educationHistories)
            {
                if (eduItem.Id == Guid.Empty)
                {
                    eduItem.Id = Guid.NewGuid();
                    employee.EmployeeEducationHistory.Add(eduItem);
                }
                else
                {
                    var educationHistory = _educationHistoryUnitOfWork.EducationHistoryRepository.GetById(eduItem.Id);
                    educationHistory.Degree = eduItem.Degree;
                    educationHistory.InstituteName = eduItem.InstituteName;
                    educationHistory.PassingYear = eduItem.PassingYear;
                    educationHistory.Result = eduItem.Result;
                    educationHistory.Subject = eduItem.Subject;
                    _educationHistoryUnitOfWork.Save();
                }
            }

            foreach (var careerHistory in careerHistories)
            {
                if (careerHistory.Id == Guid.Empty)
                {
                    careerHistory.Id = Guid.NewGuid();
                    employee.EmployeeCareerHistory.Add(careerHistory);
                }
                else
                {
                    var employeeCareerHistory =
                        _careerHistoryUnitOfWork.EducationHistoryRepository.GetById(careerHistory.Id);
                    employeeCareerHistory.CompanyName = careerHistory.CompanyName;
                    employeeCareerHistory.CompanyAddress = careerHistory.CompanyAddress;
                    employeeCareerHistory.Department = careerHistory.Department;
                    employeeCareerHistory.Position = careerHistory.Position;
                    employeeCareerHistory.StartDate = careerHistory.StartDate;
                    employeeCareerHistory.EndDate = careerHistory.EndDate;
                    _careerHistoryUnitOfWork.Save();

                }
            }
          

            _employeeUnitOfWork.Save();
        }
    }
}
