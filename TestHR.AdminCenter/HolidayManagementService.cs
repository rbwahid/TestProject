using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class HolidayManagementService
    {
        private AdminCenterDbContext _context;
        private HolidayUnitOfWork _holidayUnitOfWork;
        private BranchUnitOfWork _branchUnitOfWork;
        private CompanyUnitOfWork _companyUnitOfWork;

        public HolidayManagementService()
        {
            _context = new AdminCenterDbContext();
            _holidayUnitOfWork = new HolidayUnitOfWork(_context);
            _branchUnitOfWork = new BranchUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
        }

        public List<Holiday> GetAllHoliday()
        {
            return _holidayUnitOfWork.HolidayRepository.GetAll().ToList();
        }

        public Holiday GetHoliday(Guid id)
        {
            return _holidayUnitOfWork.HolidayRepository.GetById(id);
        }

         public void DeleteHoliday(Guid id)
        {
            _holidayUnitOfWork.HolidayRepository.DeleteById(id);
            _holidayUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _holidayUnitOfWork.HolidayRepository.GetAll().Count();
            
        }
        public void AddHoliday(string Name, string Description, DateTime? DateFrom, DateTime? DateTo)
        {
            var holiday = new Holiday();

            holiday.Name = Name;
            holiday.Description = Description;
            holiday.DateFrom = DateFrom.Value;
            holiday.DateTo = DateTo.Value;
            _holidayUnitOfWork.HolidayRepository.Add(holiday);
            _holidayUnitOfWork.Save();

        }
    }
}
