using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Services.Description;
using OfficeOpenXml;
using TestHR.Entities;
using TestHR.AdminCenter;

namespace TestHR.Web.Areas.Admin.Models
{
    public class AttendanceLogModel
    {
        private AttendanceLogManagementService _attendanceLogManagementService { get; set; }

        public int? FPId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan PunchTime { get; set; }
        public AttendanceLogModel()
        {
            _attendanceLogManagementService = new AttendanceLogManagementService();
        }
        public List<AttendanceLog> GetAllaAttendanceLogs()
        {

            return _attendanceLogManagementService.GetAllAttendanceLog();
        }

       
        public void AddToAttendanceLog()
        {
            _attendanceLogManagementService.AddToAttendanceLog(FPId.Value, AttendanceDate);

        }

        public void PoolLog()
        {
            _attendanceLogManagementService.DeviceSync();
            //_attendanceLogManagementService.InstallSdk();
            
           
        }

      

        
        internal void DeleteAttendanceLog(Guid? id)
        {
            if (id.HasValue)
            {
                _attendanceLogManagementService.DeleteAttendanceLog(id.Value);
            }                
            else
            {
                throw new Exception();
            }
        }

        public AttendanceLog LoadAttendanceLog(Guid? id)
        {
            if (id.HasValue)
            {
                return _attendanceLogManagementService.GetAttendanceLog(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
}