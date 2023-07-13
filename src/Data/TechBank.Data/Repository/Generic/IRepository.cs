using System.Linq.Expressions;

namespace TechBank.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        T? GetFirstOrDefault(Expression<Func<T, bool>> expression);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
