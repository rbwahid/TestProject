using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class LeaveTypeModel
    {
        private LeaveTypeManagementService _leaveTypeManagementService { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int NumberOfDays { get; set; }

       public LeaveTypeModel()
        {
           _leaveTypeManagementService = new LeaveTypeManagementService();
        }
        public List<LeaveType> GetAllLeaveTypes()
        {
           
            return _leaveTypeManagementService.GetAllLeaveTypes();
        }
      
          public LeaveTypeModel(Guid id) : this()
        {
            var leaveType = _leaveTypeManagementService.GetLeaveType(id);

            this.Id = leaveType.Id;
            this.Name = leaveType.Name;

              this.NumberOfDays = leaveType.NumberOfDays;


        }
        public void AddLeaveType()
        {
            _leaveTypeManagementService.AddLeaveType(Name, NumberOfDays);
        }

        public void EditLeaveType()
        {
    
            _leaveTypeManagementService.EditLeaveType(Id,Name,NumberOfDays);
        
        }
        internal void DeleteLeave(Guid? id)
        {
            if (id.HasValue)
            {
                _leaveTypeManagementService.DeleteLeaveType(id.Value);
            }                
            else
            {
                throw new Exception();
            }
                
        }

        public LeaveType LoadLeaveTypeData(Guid? id)
        {
            if (id.HasValue)
            {
                return _leaveTypeManagementService.GetLeaveType(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    
    }
}