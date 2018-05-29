using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestHR.Entities
{
    [Table("Roles")]
    public class Role : Entity
    {
        [Display(Name="Role Name")]
        public string RoleName { get; set; }
        public virtual ICollection<RoleTask> RoleTasks { get; set; }
    }
    [Table("RoleTasks")]
    public class RoleTask:Entity
    {
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public string Task { get; set; }

    }
}
