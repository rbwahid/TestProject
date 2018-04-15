using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
  public  class LeaveApplicationManagementService
    {
          private AdminCenterDbContext _context;
        private LeaveTypeUnitOfWork _leaveTypeUnitOfWork;
        private LeaveApplicationUnitOfWork _leaveApplicationUnitOfWork;
      private EmployeeUnitOfWork _employeeUnitOfWork;


        public LeaveApplicationManagementService()
        {
            _context = new AdminCenterDbContext();
           _leaveTypeUnitOfWork = new LeaveTypeUnitOfWork(_context);
            _leaveApplicationUnitOfWork = new LeaveApplicationUnitOfWork(_context);
            _employeeUnitOfWork = new EmployeeUnitOfWork(_context);
        }
        public LeaveApplication GetLeaveApplication(Guid id)
        {
            return _leaveApplicationUnitOfWork.LeaveApplicationRepository.GetById(id);
        }

        public void DeleteLeaveApplication(Guid id)
        {
            _leaveApplicationUnitOfWork.LeaveApplicationRepository.DeleteById(id);
            _leaveApplicationUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _leaveApplicationUnitOfWork.LeaveApplicationRepository.GetAll().Count();

        }
        public List<LeaveApplication> GetAlLeaveApplications()
        {
            return _leaveApplicationUnitOfWork.LeaveApplicationRepository.GetAll().ToList();
        }
        public void AddLeaveApplication(Guid? applicantId,Guid? leaveTypeId,DateTime? dateFrom,
            DateTime? dateTo, string reasonForLeave, string addressDuringLeave, string contactDuringLeave,int? status,
            List<LeaveComment> leaveComments)
        {
            List<LeaveComment> leaveCommentList = new List<LeaveComment>();
            var leaveAppliction = new LeaveApplication()
            {
                Applicant = new Employee(),
                Approver = new Employee()
            };

            leaveAppliction.Applicant = _employeeUnitOfWork.EmployeeRepository.GetById(applicantId.Value);
            leaveAppliction.LeaveType = _leaveTypeUnitOfWork.LeaveTypeRepository.GetById(leaveTypeId.Value);
            leaveAppliction.DateFrom = dateFrom.Value;
            leaveAppliction.DateTo = dateTo.Value;
            leaveAppliction.ReasonForLeave = reasonForLeave;
            leaveAppliction.AddressDuringLeave = addressDuringLeave;
            leaveAppliction.ContactDuringLeave = contactDuringLeave;
            leaveAppliction.Status = status.Value;
            foreach (var item in leaveComments)
            {
                LeaveComment leaveComment = new LeaveComment();
                leaveComment.Employee = _employeeUnitOfWork.EmployeeRepository.GetById(item.Employee.Id);
                leaveComment.Comment = item.Comment;
                leaveComment.CommentDate = DateTime.Now;
                leaveCommentList.Add(leaveComment);
            }
            leaveAppliction.LeaveComments = leaveCommentList;
            _leaveApplicationUnitOfWork.LeaveApplicationRepository.Add(leaveAppliction);
            _leaveApplicationUnitOfWork.Save();
        }
    }
}
