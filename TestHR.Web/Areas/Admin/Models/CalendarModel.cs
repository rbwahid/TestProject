using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class CalendarModel
    {
        private CalendarManagementService _calendarManagementService { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public CalendarModel()
        {
            _calendarManagementService = new CalendarManagementService();
        }
        public List<Holiday> GetAllHolidays()
        {

            return _calendarManagementService.GetAllHolidays();
        }

       
        public void AddToCalendar()
        {
            _calendarManagementService.AddToCalendar(Name, Description, DateFrom, DateTo);
        }
       
        internal void DeleteHoliday(Guid? id)
        {
            if (id.HasValue)
            {
                _calendarManagementService.DeleteHoliday(id.Value);
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
                return _calendarManagementService.GetCalendar(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
    
}