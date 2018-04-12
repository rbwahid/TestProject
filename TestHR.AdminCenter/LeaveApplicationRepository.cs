using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
  public class LeaveApplicationRepository:Repository<LeaveApplication>
    {
       private AdminCenterDbContext _context;
       public LeaveApplicationRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
