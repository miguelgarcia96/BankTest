using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TechBank.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WebAppContext _appContext;
        internal DbSet<T> dbSet;

        public Repository(WebAppContext appContext)
        {
            _appContext = appContext;
            dbSet = _appContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(expression);

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
