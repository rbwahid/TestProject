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

        public virtual List<Employee> Employees { get; set; }
        public virtual List<LeaveType> LeaveTypes { get; set; }
        public string AddressDuringLeave { get; set; }
        public string ContactDuringLeave { get; set; }
        public int? Status { get; set; }
        public string Comments { get; set; }



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
            return _leaveTypeManagementService.GetAllLeaveTypes().Where(e => e.IsDelete == false).ToList();
        }

        private List<Employee> GetAllEmployee()
        {
            return _employeeManagementService.GetAllEmployees().Where(e => e.IsDelete == false).ToList();
        }
        public List<LeaveApplication> GetLeaveApplications()
        {

            return _leaveApplicationManagementService.GetAlLeaveApplications().Where(e => e.IsDelete == false).ToList();
        }

        public LeaveApplicationModel(Guid id)
            : this()
        {
            var leaveApplication = _leaveApplicationManagementService.GetLeaveApplication(id);
            this.Id = leaveApplication.Id;
            this.ReasonForLeave = leaveApplication.ReasonForLeave;
            this.ApplicantId = leaveApplication.Applicant.Id;
            this.DateFrom = leaveApplication.DateFrom;
            this.DateTo = leaveApplication.DateTo;
            this.LeaveTypeId = leaveApplication.LeaveType.Id;
            this.AddressDuringLeave = leaveApplication.AddressDuringLeave;
            this.ContactDuringLeave = leaveApplication.ContactDuringLeave;

        }
        public void AddLeaveApplication()
        {

            _leaveApplicationManagementService.AddLeaveApplication(ApplicantId, LeaveTypeId, DateFrom, DateTo, ReasonForLeave,
                AddressDuringLeave, ContactDuringLeave);
        }
        public void EditLeaveApplication()
        {

            _leaveApplicationManagementService.EditLeaveApplication(Id, ApplicantId, LeaveTypeId, DateFrom, DateTo, ReasonForLeave,
                AddressDuringLeave, ContactDuringLeave);
        }

        public void LeaveApprove()
        {

            _leaveApplicationManagementService.LeaveApprove(Id, Status.Value, Comments);
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

        public List<LeaveReportViewModel> LeaveReport()
        {
            List<LeaveReportViewModel> leaveReportViewModels = new List<LeaveReportViewModel>();
           
            foreach (var employee in _employeeManagementService.GetAllEmployees().Where(e=>e.IsDelete==false).ToList())
            {
                 LeaveReportViewModel leaveReportView = new LeaveReportViewModel();
                leaveReportView.Employee = employee;
                List<LeaveType> leaveTypesList = new List<LeaveType>();
                foreach (var leaveType in _leaveTypeManagementService.GetAllLeaveTypes().Where(e=>e.IsDelete==false))
                {
                    LeaveType leaveTy = new LeaveType();
                    leaveTy.NumberOfDays = 0;
                    foreach (var leave in _leaveApplicationManagementService.GetAlLeaveApplications().Where(e=>e.IsDelete==false && e.Status==3 && e.Applicant.Id== employee.Id && e.LeaveType.Id== leaveType.Id).ToList())
                    {
                        while (leave.DateFrom!= leave.DateTo)
                        {
                            leaveTy.NumberOfDays++;
                            leave.DateFrom = leave.DateFrom.AddDays(1);
                        }
                    }
                    leaveTypesList.Add(leaveTy);
                }
                leaveReportView.LeaveType = leaveTypesList;
                leaveReportViewModels.Add(leaveReportView);
            }
            return leaveReportViewModels;
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

    public class LeaveReportViewModel
    {
        public virtual Employee Employee { get; set; }

        public virtual List<LeaveType> LeaveType { get; set; }
    }
}