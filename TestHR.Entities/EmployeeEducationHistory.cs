using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class EmployeeEducationHistory : Entity
    {
        public string Degree { get; set; }
        public string InstituteName { get; set; }
        public string Subject { get; set; }
        public DateTime PassingYear { get; set; }
        public string Result { get; set; }        
    }
}
