using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class CalenderManagementService
    {
        private AdminCenterDbContext _context;
        private CalenderUnitOfWork _calenderUnitOfWork;

        public CalenderManagementService()
        {
            _context = new AdminCenterDbContext();
            _calenderUnitOfWork = new CalenderUnitOfWork(_context);
        }

        public List<Holiday> GetAllHolidays()
        {
            return _calenderUnitOfWork.CalenderRepository.GetAll().ToList();
        }

        public Holiday GetCalender(Guid id)
        {
            return _calenderUnitOfWork.CalenderRepository.GetById(id);
        }
         public void DeleteHoliday(Guid id)
        {
            _calenderUnitOfWork.CalenderRepository.DeleteById(id);
            _calenderUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _calenderUnitOfWork.CalenderRepository.GetAll().Count();
            
        }
        public void AddToCalender(string Name, string Description, DateTime? DateFrom, DateTime? DateTo)
        {
            var holidayInfo = new Holiday();

            holidayInfo.Name = Name;
            holidayInfo.Description = Description;
            holidayInfo.DateFrom = DateFrom.Value;
            holidayInfo.DateTo = DateTo.Value;
            _calenderUnitOfWork.CalenderRepository.Add(holidayInfo);
            _calenderUnitOfWork.Save();

        }

    }
}
