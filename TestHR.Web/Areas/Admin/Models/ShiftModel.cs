using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OfficeOpenXml.FormulaParsing.Utilities;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class ShiftModel
    {
        private ShiftManagementService _shiftManagementService { get; set; }
        private CompanyManagementService _companyManagementService { get; set; }
        private BranchManagementService _branchManagementService { get; set; }
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters!")]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
        [DisplayName("Default")]
        public bool IsDefault { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; }
        [Required]
        public string OfficeHourDescription { get; set; }
        public  List<TimeTable> TimeTables { get; set; }
        public int GraceTimeIn { get; set; }
        public int GraceTimeOut { get; set; }
        public int OvertimeStart { get; set; }
        public ShiftModel()
        {
            _companyManagementService = new CompanyManagementService();
            _branchManagementService = new BranchManagementService();
            _shiftManagementService =new ShiftManagementService();
        }
        public List<Company> GetAllCompanies()
        {
           
            return _companyManagementService.GetAllCompanies();
        }
      
        public ShiftModel(Guid id)
            : this()
        {
            var shift = _shiftManagementService.GetShift(id);

            this.Id = shift.Id;
            this.Name = shift.Name;
            this.Code = shift.Code;
            this.Type = shift.Type;
            this.Description = shift.Description;
            this.IsDefault = shift.IsDefault;
            this.IsActive = shift.IsActive;
            this.OfficeHourDescription = shift.OfficeHourDescription;
            this.TimeTables = shift.TimeTables.ToList();
            this.GraceTimeIn = shift.GraceTimeIn.Value;
            this.GraceTimeOut = shift.GraceTimeOut.Value;
            this.OvertimeStart = shift.OvertimeStart.Value;

        }
        public void AddShift()
        {
            _shiftManagementService.AddShift( Name, Code, Type, Description, IsDefault, IsActive,OfficeHourDescription,TimeTables,GraceTimeIn,GraceTimeOut,OvertimeStart);
        }

        public void EditShift(Guid id)
        {
            _shiftManagementService.EditShift(id, Name, Code, Type, Description, IsDefault, IsActive, OfficeHourDescription, TimeTables, GraceTimeIn, GraceTimeOut, OvertimeStart);
        }
        public List<Shift> GetAllShift()
        {
            return _shiftManagementService.GetAllShifts();
        }
        internal void DeleteShift(Guid? id)
        {
            if (id.HasValue)
            {
                _shiftManagementService.DeleteShift(id.Value);
            }                
            else
            {
                throw new Exception();
            }
                
        }

        public Shift LoadShiftData(Guid? id)
        {
            if (id.HasValue)
            {
                return _shiftManagementService.GetShift(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
}