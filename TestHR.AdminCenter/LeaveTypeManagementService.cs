using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class LeaveTypeManagementService
    {
        private AdminCenterDbContext _context;
        private LeaveTypeUnitOfWork _leaveTypeUnitOfWork;
          public LeaveTypeManagementService()
        {
            _context = new AdminCenterDbContext();
            _leaveTypeUnitOfWork = new LeaveTypeUnitOfWork(_context);
        }

        public List<LeaveType> GetAllLeaveTypes()
        {
            return _leaveTypeUnitOfWork.LeaveTypeRepository.GetAll().ToList();
        }

        public LeaveType GetLeaveType(Guid id)
        {
            return _leaveTypeUnitOfWork.LeaveTypeRepository.GetById(id);
        }

         public void DeleteLeaveType(Guid id)
        {
            _leaveTypeUnitOfWork.LeaveTypeRepository.DeleteById(id);
            _leaveTypeUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _leaveTypeUnitOfWork.LeaveTypeRepository.GetAll().Count();
            
        }
        public void AddLeaveType(string name, int numberOfDays)
        {
            var leaveType = new LeaveType();

            leaveType.Name = name;
            leaveType.NumberOfDays = numberOfDays;
            _leaveTypeUnitOfWork.LeaveTypeRepository.Add(leaveType);
            _leaveTypeUnitOfWork.Save();

        }
    }
}
