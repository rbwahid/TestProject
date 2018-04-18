using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class TimeTable : Entity
    {
        public bool Status { get; set; }
        public DayOfWeek? Day { get; set; }
        public TimeSpan? From { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan? To { get; set; }
    }
}
