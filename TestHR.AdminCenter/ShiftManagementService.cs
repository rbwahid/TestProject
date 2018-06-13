using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class ShiftManagementService
    {
        private AdminCenterDbContext _context;
        private ShiftUnitOfWork _shifthUnitOfWork;
        private BranchUnitOfWork _branchUnitOfWork;
        private CompanyUnitOfWork _companyUnitOfWork;
        private TimeTableUnitOfWork _timeTableUnitOfWork;
        public ShiftManagementService()
        {
            _context = new AdminCenterDbContext();
            _shifthUnitOfWork = new ShiftUnitOfWork(_context);
            _branchUnitOfWork = new BranchUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
            _timeTableUnitOfWork = new TimeTableUnitOfWork(_context);
        }

        public List<Shift> GetAllShifts()
        {
            return _shifthUnitOfWork.ShiftRepository.GetAll().ToList();
        }

        public Shift GetShift(Guid id)
        {
            Shift shift = _shifthUnitOfWork.ShiftRepository.GetById(id);
            return shift;
        }

         public void DeleteShift(Guid id)
        {
            _shifthUnitOfWork.ShiftRepository.DeleteById(id);
            _shifthUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _shifthUnitOfWork.ShiftRepository.GetAll().Count();
            
        }
        public void AddShift(string name, string code, string type, string description, bool isDefault, bool isActive, string officeHourDescription, List<TimeTable> timeTables, int graceTimeIn, int GraceTimeOut, int overtimeStart)
        {
            var shift = new Shift();

            shift.Name = name;
            shift.Code = code;
            shift.Type = type;
            shift.Description = description;
            shift.IsDefault = isDefault;
            shift.IsActive = isActive;
            shift.OfficeHourDescription = officeHourDescription;
            shift.TimeTables = timeTables;
            shift.GraceTimeIn = graceTimeIn;
            shift.GraceTimeOut = GraceTimeOut;
            shift.OvertimeStart = overtimeStart;
            _shifthUnitOfWork.ShiftRepository.Add(shift);
            _shifthUnitOfWork.Save();

        }
        public void EditShift(Guid id, string name, string code, string type, string description, bool isDefault, bool isActive, string officeHourDescription, List<TimeTable> timeTables, int graceTimeIn, int GraceTimeOut, int overtimeStart)
        {
            var shift = GetShift(id);

            shift.Name = name;
            shift.Code = code;
            shift.Type = type;
            shift.Description = description;
            shift.IsDefault = isDefault;
            shift.IsActive = isActive;
            shift.OfficeHourDescription = officeHourDescription;
            shift.GraceTimeIn = graceTimeIn;
            shift.GraceTimeOut = GraceTimeOut;
            shift.OvertimeStart = overtimeStart;
            _shifthUnitOfWork.Save();
    
            foreach (var timeTable in timeTables)
            {
               
                    var objTimeTable = _timeTableUnitOfWork.TimeTableRepository.GetById(timeTable.Id);
                    objTimeTable.Day = timeTable.Day;
                    objTimeTable.Status = timeTable.Status;
                    objTimeTable.From = timeTable.From;
                    objTimeTable.To = timeTable.To;
                    _timeTableUnitOfWork.Save();
                
            }
         

        }
    }
}
