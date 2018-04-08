using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestHR.Entities
{
    public class Department : Entity
    {
        public Company Company { get; set; }
        public string Name { get; set; }
        public virtual Employee DepartmentHead { get; set; }
    }
}
