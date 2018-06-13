using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    class CalendarRepository : Repository<Holiday>
    {
        private AdminCenterDbContext _context;
        public CalendarRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
