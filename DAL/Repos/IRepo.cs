using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Repos
{
    public interface IRepo<T> : IReadOnlyRepo<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
    public interface IReadOnlyRepo<T> where T : class
    {
        T Get(Expression<Func<T, bool>> predicate);
        T GetAsNoTracking(Expression<Func<T, bool>> predicate);
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
