using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
   public class RoleRepository: Repository<Role>
   {
       private AdminCenterDbContext _context;
       public RoleRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
   public class RoleTaskRepository : Repository<RoleTask>
   {
       private AdminCenterDbContext _context;
       public RoleTaskRepository(AdminCenterDbContext context)
           : base(context)
       {
           _context = context;
       }
   }  
}
