using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
//using System.Linq.Dynamic;

namespace TestHR.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private DbContext _context ;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Where(x => x.Id == id).SingleOrDefault();
        }

        public void DeleteById(Guid id)
        {
            T item = GetById(id);
            DeleteByItem(item);
        }

        public void DeleteByItem(T item)
        {
            _context.Set<T>().Remove(item);
        }
    }
}
