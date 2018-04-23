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
        public PositionManagementService()
        {
            _context = new AdminCenterDbContext();
            _positionUnitOfWork = new PositionUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
            _departmentUnitOfWork = new DepartmentUnitOfWork(_context);
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
        public void AddPosition(string name, Guid companyId,Guid departmentId,Guid reportingPositionId)
        {
            var position = new Position();

            position.Name = name;
            position.Company = _companyUnitOfWork.CompanyRepository.GetById(companyId);
            position.Department = _departmentUnitOfWork.DepartmentRepository.GetById(departmentId);
            position.ReportingPosition = _positionUnitOfWork.PositionRepository.GetById(reportingPositionId);
            _positionUnitOfWork.PositionRepository.Add(position);
            _positionUnitOfWork.Save();

        }
        public void EditPosition(Guid id,string name, Guid companyId, Guid departmentId, Guid reportingPositionId)
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
