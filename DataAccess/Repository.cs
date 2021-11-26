using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DataContext context;
        private DbSet<T> entities;
        protected virtual DbSet<T> Entities => entities ??= context.Set<T>();

        public Repository(DataContext dataContext)
        {
            context = dataContext;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.Where(predicate).FirstOrDefault();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public virtual int Save()
        {
            var affected = context.SaveChanges();
            return affected;
        }


    }
}
