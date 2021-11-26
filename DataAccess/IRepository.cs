using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        int Save();
    }
}