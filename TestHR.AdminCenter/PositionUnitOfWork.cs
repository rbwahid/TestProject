using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
   public class PositionUnitOfWork : IDisposable
    {
       private AdminCenterDbContext _context { get; set; }
       private PositionRepository _positionRepository { get; set; }
       public PositionUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _positionRepository = new PositionRepository(_context);
        }

       public PositionRepository PositionRepository
        {
            get
            {
                return _positionRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
