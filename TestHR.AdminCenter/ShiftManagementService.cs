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

        public ShiftManagementService()
        {
            _context = new AdminCenterDbContext();
            _shifthUnitOfWork = new ShiftUnitOfWork(_context);
            _branchUnitOfWork = new BranchUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
        }

        public List<Shift> GetAllShifts()
        {
            return _shifthUnitOfWork.ShiftRepository.GetAll().ToList();
        }

        public Shift GetShift(Guid id)
        {
            return _shifthUnitOfWork.ShiftRepository.GetById(id);
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
        public void AddShift(string Name, string Code, string Type, string Description, bool IsDefault, bool IsActive, string OfficeHourDescription)
        {
            var shift = new Shift();

            shift.Name = Name;
            shift.Code = Code;
            shift.Type = Type;
            shift.Description = Description;
            shift.IsDefault = IsDefault;
            shift.IsActive = IsActive;
            shift.OfficeHourDescription = OfficeHourDescription;
            _shifthUnitOfWork.ShiftRepository.Add(shift);
            _shifthUnitOfWork.Save();

        }
    }
}
