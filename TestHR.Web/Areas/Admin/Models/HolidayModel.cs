using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class HolidayModel
    {
        private HolidayManagementService _holidayManagementService { get; set; }
        private CompanyManagementService _companyManagementService { get; set; }
        private BranchManagementService _branchManagementService { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public HolidayModel()
        {
            _holidayManagementService = new HolidayManagementService();
            _companyManagementService = new CompanyManagementService();
            _branchManagementService = new BranchManagementService();
            
        }
        
    
          public HolidayModel(Guid id) : this()
        {
            var holiday = _holidayManagementService.GetHoliday(id);

            this.Id = holiday.Id;
            this.Name = holiday.Name;
            this.Description = holiday.Description;
            this.DateFrom = holiday.DateFrom;
            this.DateTo = holiday.DateTo;
        }
        public void AddHoliday()
        {
            _holidayManagementService.AddHoliday(Name, Description, DateFrom,DateTo);
        }

        internal void DeleteHoliday(Guid? id)
        {
            if (id.HasValue)
            {
                _holidayManagementService.DeleteHoliday(id.Value);
            }                
            else
            {
                throw new Exception();
            }
                
        }

        public Holiday LoadHolidayData(Guid? id)
        {
            if (id.HasValue)
            {
                return _holidayManagementService.GetHoliday(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
}