using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
   public class EmployeeRepository: Repository<Employee>
   {
       private AdminCenterDbContext _context;
       public EmployeeRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }  
    public class EducationHistoryRepository: Repository<EmployeeEducationHistory>
   {
       private AdminCenterDbContext _context;
       public EducationHistoryRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    } 
    public class CareerHistoryRepository: Repository<EmployeeCareerHistory>
   {
       private AdminCenterDbContext _context;
        
       public CareerHistoryRepository(AdminCenterDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
