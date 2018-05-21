using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    class AttendanceRepository:Repository<Attendance>
    {
        private  AdminCenterDbContext _context;
        public AttendanceRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
