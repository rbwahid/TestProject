using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class LeaveComment : Entity
    {
        public Employee Employee { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }
    }
}
