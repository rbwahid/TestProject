using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class PositionManagementService
    {
        private AdminCenterDbContext _context;
        private PositionUnitOfWork _positionUnitOfWork;
        private CompanyUnitOfWork _companyUnitOfWork;
        private DepartmentUnitOfWork _departmentUnitOfWork;
        private CompanyManagementService _companyManagementService;
        private DepartmentManagementService _departmentManagementService;
        private PositionManagementService _positionManagementService;
        public PositionManagementService()
        {
            _context = new AdminCenterDbContext();
            _positionUnitOfWork = new PositionUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
            _departmentUnitOfWork = new DepartmentUnitOfWork(_context);
        }
        public void AddToPositionExcel(string companyName, string departmentName, string name, string reportingPosName)
        {
            var company = _companyUnitOfWork.CompanyRepository.GetAll().FirstOrDefault(e => e.Name == companyName);
            var department = _departmentUnitOfWork.DepartmentRepository.GetAll().FirstOrDefault(e => e.Name == departmentName);
            var position = _positionUnitOfWork.PositionRepository.GetAll().FirstOrDefault(e => e.Name == name);
            var reportPosition = _positionUnitOfWork.PositionRepository.GetAll().FirstOrDefault(e => e.Name == reportingPosName);
            if (position == null)
            {
                if (company != null)
                {
                    if (department != null)
                    {
                        if (reportPosition == null)
                        {
                            AddPosition(name, company.Id, department.Id, Guid.Empty);
                        }
                        else
                        {
                            AddPosition(name, company.Id, department.Id, reportPosition.Id);
                        }
                    }
                    else
                    {
                        _departmentManagementService = new DepartmentManagementService();
                        _departmentManagementService.AddDepartment(departmentName, company.Id, Guid.Empty);
                        AddPosition(name,
                            _companyUnitOfWork.CompanyRepository.GetAll().FirstOrDefault(e => e.Name == companyName).Id,
                            _departmentUnitOfWork.DepartmentRepository.GetAll()
                                .FirstOrDefault(e => e.Name == departmentName)
                                .Id,
                            Guid.Empty
                            );
                    }
                }
                else
                {
                    _companyManagementService = new CompanyManagementService();
                    _companyManagementService.AddCompany(companyName, Guid.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    AddPosition(name,
                        _companyUnitOfWork.CompanyRepository.GetAll().FirstOrDefault(e => e.Name == companyName).Id,
                        _departmentUnitOfWork.DepartmentRepository.GetAll().FirstOrDefault(e => e.Name == departmentName).Id,
                        Guid.Empty
                      );
                }
            }
        }
        public List<Position> GelAllPositions()
        {
            return _positionUnitOfWork.PositionRepository.GetAll().ToList();
        }

        public Position GetPosition(Guid id)
        {
            return _positionUnitOfWork.PositionRepository.GetById(id);
        }

        public void DeletePosition(Guid id)
        {
            _positionUnitOfWork.PositionRepository.DeleteById(id);
            _positionUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _positionUnitOfWork.PositionRepository.GetAll().Count();

        }
        public void AddPosition(string name, Guid companyId, Guid departmentId, Guid reportingPositionId)
        {
            var position = new Position();

            position.Name = name;
            position.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId);
            position.Department = _departmentUnitOfWork.DepartmentRepository.GetById(departmentId);
            position.ReportingPosition = _positionUnitOfWork.PositionRepository.GetById(reportingPositionId);
            _positionUnitOfWork.PositionRepository.Add(position);
            _positionUnitOfWork.Save();

        }
        public void EditPosition(Guid id, string name, Guid companyId, Guid departmentId, Guid reportingPositionId)
        {
            var position = GetPosition(id);

            position.Name = name;
            position.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId);
            position.Department = _departmentUnitOfWork.DepartmentRepository.GetById(departmentId);
            position.ReportingPosition = _positionUnitOfWork.PositionRepository.GetById(reportingPositionId);

            _positionUnitOfWork.Save();

        }
    }
}
