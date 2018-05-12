using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class AttendanceModel
    {
        public Guid Id { get; set; }
        public List<Employee> Employees { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan EntryTime { get; set; }
        public TimeSpan? ExitTime { get; set; }
        public TimeSpan? LateEntry { get; set; }
        public TimeSpan? EarlyExit { get; set; }
        public TimeSpan? Overtime { get; set; }
    }
}