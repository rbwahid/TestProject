using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestHR.Data
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T GetByID(Guid id);

        void DeleteByID(Guid id);

        void DeleteByItem(T item);
    }
}