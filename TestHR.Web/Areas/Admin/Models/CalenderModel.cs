using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class CalenderModel
    {
        private CalenderManagementService _calenderManagementService { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public CalenderModel()
        { 
            _calenderManagementService = new CalenderManagementService();
        }
        public List<Holiday> GetAllHolidays()
        {
           
            return _calenderManagementService.GetAllHolidays();
        }

       
        public void AddToCalender()
        {
            _calenderManagementService.AddToCalender(Name, Description, DateFrom, DateTo);
        }
       
        internal void DeleteHoliday(Guid? id)
        {
            if (id.HasValue)
            {
                _calenderManagementService.DeleteHoliday(id.Value);
            }                
            else
            {
                throw new Exception();
            }
                
        }

        public Holiday LoadCalender(Guid? id)
        {
            if (id.HasValue)
            {
                return _calenderManagementService.GetCalender(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
    
}