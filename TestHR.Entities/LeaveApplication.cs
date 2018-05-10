using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestHR.Entities
{
    public class LeaveApplication : Entity
    {
        [Required]
        public virtual Employee Applicant { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string ReasonForLeave { get; set; }
        public string AddressDuringLeave { get; set; }
        public string ContactDuringLeave { get; set; }
        public virtual Employee Approver { get; set; }
        public int Status { get; set; }
        public virtual List<LeaveComment> LeaveComments { get; set; }
    }
}
