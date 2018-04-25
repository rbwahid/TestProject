﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    class CalenderRepository : Repository<Holiday>
    {
        private AdminCenterDbContext _context;
        public CalenderRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
