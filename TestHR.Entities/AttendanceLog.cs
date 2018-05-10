using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class AttendanceLog : Entity
    {
        //public virtual Employee Employee { get; set; }
        public string Name { get; set; }
        public DateTime AttendanceDate{ get; set; }
        public TimeSpan PunchTime { get; set; }

    }
}
