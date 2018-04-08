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
    public class CompanyRepository : Repository<Company>
    {
        private AdminCenterDbContext _context;
        public CompanyRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
