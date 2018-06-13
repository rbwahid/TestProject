using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class Attendance : Entity 
    {
        public  virtual Employee Employee { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan EntryTime { get; set; }
        public TimeSpan? ExitTime { get; set; }
        public TimeSpan? LateEntry { get; set; }
        public TimeSpan? EarlyExit { get; set; }
        public TimeSpan? Overtime { get; set; }
    }
}
