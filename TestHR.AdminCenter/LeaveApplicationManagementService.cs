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

      public void LeaveApprove(Guid id,int status,string comments)
      {
          var leaveApplication = GetLeaveApplication(id);
          leaveApplication.Status = status;
          LeaveComment leaveComment = new LeaveComment();
          leaveComment.Comment = comments;
          leaveComment.CommentDate = DateTime.Now.Date;
          leaveComment.Employee = leaveApplication.Approver;
          leaveApplication.LeaveComments.Add(leaveComment);
          _leaveApplicationUnitOfWork.Save();


      }
        public List<LeaveApplication> GetAlLeaveApplications()
        {
            return _leaveApplicationUnitOfWork.LeaveApplicationRepository.GetAll().ToList();
        }
        public void AddLeaveApplication(Guid? applicantId,Guid? leaveTypeId,DateTime? dateFrom,
            DateTime? dateTo, string reasonForLeave, string addressDuringLeave, string contactDuringLeave)
        {
            var leaveAppliction = new LeaveApplication();
           
            leaveAppliction.Applicant = _employeeUnitOfWork.EmployeeRepository.GetById(applicantId.Value);
            leaveAppliction.Approver = leaveAppliction.Applicant.ReportingTo;
            leaveAppliction.LeaveType = _leaveTypeUnitOfWork.LeaveTypeRepository.GetById(leaveTypeId.Value);
            leaveAppliction.DateFrom = dateFrom.Value;
            leaveAppliction.DateTo = dateTo.Value;
            leaveAppliction.ReasonForLeave = reasonForLeave;
            leaveAppliction.AddressDuringLeave = addressDuringLeave;
            leaveAppliction.ContactDuringLeave = contactDuringLeave;
            leaveAppliction.Status = 1;
            _leaveApplicationUnitOfWork.LeaveApplicationRepository.Add(leaveAppliction);
            _leaveApplicationUnitOfWork.Save();
        }

      public void EditLeaveApplication(Guid id, Guid? applicantId, Guid? leaveTypeId, DateTime? dateFrom, DateTime? dateTo, string reasonForLeave, string addressDuringLeave, string contactDuringLeave)
      {
          var leaveAppliction = GetLeaveApplication(id);
          leaveAppliction.Approver = leaveAppliction.Applicant.ReportingTo;
          leaveAppliction.LeaveType = _leaveTypeUnitOfWork.LeaveTypeRepository.GetById(leaveTypeId.Value);
          leaveAppliction.DateFrom = dateFrom.Value;
          leaveAppliction.DateTo = dateTo.Value;
          leaveAppliction.ReasonForLeave = reasonForLeave;
          leaveAppliction.AddressDuringLeave = addressDuringLeave;
          leaveAppliction.ContactDuringLeave = contactDuringLeave;
          _leaveApplicationUnitOfWork.Save();
      }
    }
}
