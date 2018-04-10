using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class LeaveType : Entity
    {
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
    }
}
