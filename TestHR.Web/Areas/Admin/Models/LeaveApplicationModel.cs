using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{

    public class LeaveApplicationModel
    {
        private LeaveApplicationManagementService _leaveApplicationManagementService { get; set; }
        private EmployeeManagementService _employeeManagementService { get; set; }
        private LeaveTypeManagementService _leaveTypeManagementService { get; set; }
        public Guid Id { get; set; }
        public Guid? ApplicantId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    
        public string ReasonForLeave { get; set; }

        public List<Employee> Employees { get; set; }
        public List<LeaveType> LeaveTypes { get; set; } 
        public string AddressDuringLeave { get; set; }
        public string ContactDuringLeave { get; set; }
        public Guid? ApproverId { get; set; }
        public int? Status { get; set; }
        public List<LeaveComment> LeaveComments { get; set; }

        public LeaveApplicationModel()
        {
            _leaveApplicationManagementService = new LeaveApplicationManagementService();
            _employeeManagementService = new EmployeeManagementService();
            _leaveTypeManagementService = new LeaveTypeManagementService();
            Employees = GetAllEmployee();
            LeaveTypes = GetLeaveType();
        }

        private List<LeaveType> GetLeaveType()
        {
            return _leaveTypeManagementService.GetAllLeaveTypes();
        }

        private List<Employee> GetAllEmployee()
        {
            return _employeeManagementService.GetAllEmployees();
        }
        public List<LeaveApplication> GetLeaveApplications()
        {

            return _leaveApplicationManagementService.GetAlLeaveApplications();
        }

        public LeaveApplicationModel(Guid id)
            : this()
        {
            var leaveApplication = _leaveApplicationManagementService.GetLeaveApplication(id);
            this.Id = leaveApplication.Id;
            this.ApplicantId = leaveApplication.Applicant.Id;
            this.ApproverId = leaveApplication.Approver.Id;
            this.DateFrom = leaveApplication.DateFrom;
            this.DateTo = leaveApplication.DateTo;
            this.Status = leaveApplication.Status;
            this.AddressDuringLeave = leaveApplication.AddressDuringLeave;
            this.ContactDuringLeave = leaveApplication.ContactDuringLeave;
            this.LeaveComments = leaveApplication.LeaveComments;
        }
        public void AddLeaveApplication()
        {
            Status = 1;
            _leaveApplicationManagementService.AddLeaveApplication(ApplicantId, LeaveTypeId, DateFrom, DateTo, ReasonForLeave,
                AddressDuringLeave, ContactDuringLeave, Status.Value, LeaveComments);
        }

        internal void DeleteLeaveApplication(Guid? id)
        {
            if (id.HasValue)
            {
                _leaveApplicationManagementService.DeleteLeaveApplication(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }

        public LeaveApplication LoadLeaveApplication(Guid? id)
        {
            if (id.HasValue)
            {
                return _leaveApplicationManagementService.GetLeaveApplication(id.Value);
            }
            else
            {
                throw new Exception();
            }

        }
    }
}