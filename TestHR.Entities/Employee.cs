using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestHR.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string SouseName { get; set; }
        public string PhoneNumber { get; set; }
        public string PresentAddress { get; set; }
        public string PernamentAddress { get; set; }
        public string Email { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Nid { get; set; }
        public string PassportNo { get; set; }

        public int? FPId { get; set; }
        public int? CardNo { get; set; }

        public virtual Employee ReportingTo { get; set; }
        public virtual Company Company { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Position Position { get; set; }
		public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeEducationHistory> EmployeeEducationHistory { get; set; }
        public virtual ICollection<EmployeeCareerHistory> EmployeeCareerHistory { get; set; }
       
    }
}
