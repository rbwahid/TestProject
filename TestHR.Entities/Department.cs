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

        public string Name { get; set; }
        //public Guid? CompanyId { get; set; }
        //[ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
       
        //public Guid? DepartmentHeadId { get; set; }
        //[ForeignKey("DepartmentHeadId")]

        public virtual Employee DepartmentHead { get; set; }
        public ICollection<Employee> Employees { get; set; } 

    }
}
