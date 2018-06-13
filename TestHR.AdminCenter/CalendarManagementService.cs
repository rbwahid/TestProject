using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class CalendarManagementService
    {
        private AdminCenterDbContext _context;
        private CalendarUnitOfWork _calendarUnitOfWork;

        public CalendarManagementService()
        {
            _context = new AdminCenterDbContext();
            _calendarUnitOfWork = new CalendarUnitOfWork(_context);
        }

        public List<Holiday> GetAllHolidays()
        {
            return _calendarUnitOfWork.CalendarRepository.GetAll().ToList();
        }

        public Holiday GetCalendar(Guid id)
        {
            return _calendarUnitOfWork.CalendarRepository.GetById(id);
        }
         public void DeleteHoliday(Guid id)
        {
            _calendarUnitOfWork.CalendarRepository.DeleteById(id);
            _calendarUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _calendarUnitOfWork.CalendarRepository.GetAll().Count();
            
        }
        public void AddToCalendar(string Name, string Description, DateTime? DateFrom, DateTime? DateTo)
        {
            var holidayInfo = new Holiday();

            holidayInfo.Name = Name;
            holidayInfo.Description = Description;
            holidayInfo.DateFrom = DateFrom.Value;
            holidayInfo.DateTo = DateTo.Value;
            _calendarUnitOfWork.CalendarRepository.Add(holidayInfo);
            _calendarUnitOfWork.Save();

        }

    }
}
