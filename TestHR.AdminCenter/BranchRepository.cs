using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;
using System.ComponentModel.DataAnnotations;
namespace TestHR.AdminCenter
{
   public class BranchRepository : Repository<Branch>
    {
       private AdminCenterDbContext _context;
       public BranchRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
