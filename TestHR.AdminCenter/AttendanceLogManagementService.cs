using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;
using System.Web;
using System.Management.Instrumentation;
using ZkSoftwareEU;


namespace TestHR.AdminCenter
{
    public class AttendanceLogManagementService
    {
        private AdminCenterDbContext _context;
        private AttendanceLogUnitOfWork _attendanceLogUnitOfWork;
        private EmployeeUnitOfWork _employeeUnitOfWork;
        private AttendanceUnitOfWork _attendanceUnitOfWork;
        private ShiftUnitOfWork _shiftUnitOfWork;
        private CZKEUEMNetClass zkSdkClass;

        public  AttendanceLogManagementService()
        {
            _context = new AdminCenterDbContext();
            _attendanceLogUnitOfWork = new AttendanceLogUnitOfWork(_context);
            _employeeUnitOfWork = new EmployeeUnitOfWork(_context);
            _attendanceUnitOfWork= new AttendanceUnitOfWork(_context);
            _shiftUnitOfWork = new ShiftUnitOfWork(_context);
            zkSdkClass = new CZKEUEMNetClass();
        }

      
        public List<AttendanceLog> GetAllAttendanceLog()
        {
            return _attendanceLogUnitOfWork.AttendanceLogRepository.GetAll().ToList();
        }

        public AttendanceLog GetAttendanceLog(Guid id)
        {
            return _attendanceLogUnitOfWork.AttendanceLogRepository.GetById(id);
        }
         public void DeleteAttendanceLog(Guid id)
        {
            _attendanceLogUnitOfWork.AttendanceLogRepository.DeleteById(id);
            _attendanceLogUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _attendanceLogUnitOfWork.AttendanceLogRepository.GetAll().Count();
            
        }

        public void DeviceSync()
        {
            if (zkSdkClass.Connect_Net("192.168.0.101", 4370))
            {
                string enrollNumber = "";
                int verifyMode = 0;
                int inOutMode = 0;
                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minute = 0;
                int second = 0;
                int workCode = 0;
                while (zkSdkClass.SSR_GetGeneralLogData(zkSdkClass.MachineNumber,ref enrollNumber,ref verifyMode,ref inOutMode,ref year,ref month,ref day,ref hour,ref minute,ref second, ref workCode))
                {
                    AddToAttendanceLog(Convert.ToInt32(enrollNumber),new DateTime(year,month,day,hour,minute,second));
                }
            }

            AttendanceCalculate();
        }


        public void AddToAttendanceLog(int FpId, DateTime attendanceDateTime)
        {
            var employee = _employeeUnitOfWork.EmployeeRepository.GetAll().FirstOrDefault(e => e.FPId == FpId);
            if (employee != null)
            {
                if (
                    ! _attendanceLogUnitOfWork.AttendanceLogRepository.GetAll()
                        .Any(e => e.Employee.Id == employee.Id && e.AttendanceDate == attendanceDateTime))
                {
                    var attendanceInfo = new AttendanceLog();
                    attendanceInfo.Employee = _employeeUnitOfWork.EmployeeRepository.GetById(employee.Id);
                    attendanceInfo.AttendanceDate = attendanceDateTime;
                    _attendanceLogUnitOfWork.AttendanceLogRepository.Add(attendanceInfo);
                    _attendanceLogUnitOfWork.Save();

                }
            }
          
        }
        //Calculate Attendance
        public void AttendanceCalculate()
        {
            //Today Attendance Log List
            DateTime dateTimeNow = DateTime.Now.Date;
            List<AttendanceLog> attendanceLogList = _attendanceLogUnitOfWork.AttendanceLogRepository.GetAll().ToList();
            
            //Each Attendance Log Calculate
            foreach (var eachAttendanceLog in attendanceLogList)
            {
                //Search Employee Today's Attendances
                var employeeTodayAttendance =
                    _attendanceUnitOfWork.AttendanceRepository.GetAll()
                        .FirstOrDefault(
                            e =>
                                e.AttendanceDate.Date == eachAttendanceLog.AttendanceDate.Date &&
                                e.Employee.Id == eachAttendanceLog.Employee.Id);

                var shift = _shiftUnitOfWork.ShiftRepository.GetAll().FirstOrDefault();
              
                //Search Time Table And Not Holiday
                    var timeTable=  shift.TimeTables.FirstOrDefault(
                            e => e.Day == eachAttendanceLog.AttendanceDate.DayOfWeek && e.Status == true);
                
                //Check = Is Already Have intime?
                if (employeeTodayAttendance != null)
                {
                    //Set Exit Time
                    employeeTodayAttendance.ExitTime = eachAttendanceLog.AttendanceDate.TimeOfDay;

                    if (timeTable != null && timeTable.To.HasValue)
                    {
                        //LateCount And Overtime Count Set
                        TimeSpan earlyCount = timeTable.To.Value - new TimeSpan(0, shift.GraceTimeOut.Value, 0);
                        TimeSpan overTimeCount = timeTable.To.Value + new TimeSpan(0, shift.OvertimeStart.Value, 0);
                        if (employeeTodayAttendance.ExitTime < earlyCount)
                        {
                            employeeTodayAttendance.EarlyExit = earlyCount - employeeTodayAttendance.ExitTime;
                        }
                        else
                        {
                            employeeTodayAttendance.EarlyExit = null;
                        }
                        if (employeeTodayAttendance.ExitTime > overTimeCount)
                        {
                            employeeTodayAttendance.Overtime = employeeTodayAttendance.ExitTime - overTimeCount;
                        }
                        else
                        {
                            employeeTodayAttendance.Overtime = null;
                        }
                       
                       
                    }
                    _employeeUnitOfWork.Save();

                }
                else
                {
                    //New Attendance Set
                    Attendance attendance = new Attendance();
                    attendance.Employee = eachAttendanceLog.Employee;
                    attendance.AttendanceDate = eachAttendanceLog.AttendanceDate.Date;
                    attendance.EntryTime = eachAttendanceLog.AttendanceDate.TimeOfDay;
                    //Late Entry Check
                    if (timeTable != null && timeTable.From.HasValue)
                    {
                        //Late Entry Count
                        TimeSpan lateEntryCount = timeTable.From.Value + new TimeSpan(0, shift.GraceTimeIn.Value, 0);
                        if (attendance.EntryTime > lateEntryCount)
                        {
                            attendance.LateEntry = attendance.EntryTime - lateEntryCount;
                        }
                    }
                    
                    _attendanceUnitOfWork.AttendanceRepository.Add(attendance);
                    _attendanceUnitOfWork.Save();
                }

            }
        }

        //Install Sdk zkeuKeeper
        public void InstallSdk()
        {
            
            if (System.IO.File.Exists("C:\\Windows\\System32\\ZKEUEmKeeperNet.dll")==false)
            {
                string[] tempPath = HttpContext.Current.Server.MapPath("~/packages/ZKTecoProfessionalSDK.1.1.0/lib/net40/ZKEUEmKeeperNet.dll").Split('\\');
                string sdkPath = string.Empty;
                int i = 1;
                foreach (string str in tempPath)
                {
                    if (i == 1)
                    {
                        if (str != "TestHR.Web")
                        {
                            sdkPath += str;
                        }   
                    }
                    else
                    {
                        if (str != "TestHR.Web")
                        {
                            sdkPath += "/"+str;
                        } 
                    }
                    i++;

                }
              

                try
                {
                 
                    System.IO.File.Copy(sdkPath, "C:/Windows/System32/ZKEUEmKeeperNet.dll",true);
          
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                   
                }
            }
        }


    }
}
