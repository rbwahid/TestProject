using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHR.AdminCenter
{
    public class RoleUnitOfWork : IDisposable
    {
         private AdminCenterDbContext _context { get; set; }
        private RoleRepository _roleRepository { get; set; }

        public RoleUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _roleRepository = new RoleRepository(_context);
        }

        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository;
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
    public class RoleTaskUnitOfWork : IDisposable
    {
        private AdminCenterDbContext _context { get; set; }
        private RoleTaskRepository _roleTaskRepository { get; set; }

        public RoleTaskUnitOfWork(AdminCenterDbContext context)
        {
            _context = context;
            _roleTaskRepository = new RoleTaskRepository(_context);
        }

        public RoleTaskRepository RoleTaskRepository
        {
            get
            {
                return _roleTaskRepository;
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
