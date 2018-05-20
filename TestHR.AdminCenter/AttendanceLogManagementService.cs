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
        private CZKEUEMNetClass zkSdkClass;

        public  AttendanceLogManagementService()
        {
            _context = new AdminCenterDbContext();
            _attendanceLogUnitOfWork = new AttendanceLogUnitOfWork(_context);
            _employeeUnitOfWork = new EmployeeUnitOfWork(_context);
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
                    AddToAttendanceLog(Convert.ToInt16(enrollNumber),new DateTime(year,month,day,hour,minute,second));
                }
            }
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
