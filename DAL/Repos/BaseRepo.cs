using Common.Authentication;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repos
{
    public class BaseRepo<T> : BaseReadOnlyRepo<T>, IRepo<T> where T : class
    {
        public BaseRepo(IAuthenticatedUser authenticatedUser, MovieSaaSContext saasDB)
            :base(authenticatedUser, saasDB)
        {
        }
        
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        //public void Update(T entity)
        //{
        //    _dbSet.Attach(entity);
        //    _saasDB.Entry(entity).State = EntityState.Modified;
        //}
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
    public abstract class BaseReadOnlyRepo<T> : IReadOnlyRepo<T> where T : class
    {
        private readonly IAuthenticatedUser _authenticatedUser;

        protected MovieSaaSContext _saasDB;
        protected DbSet<T> _dbSet;
        public BaseReadOnlyRepo(IAuthenticatedUser authenticatedUser, MovieSaaSContext saasDB)
        {
            _authenticatedUser = authenticatedUser;
            _saasDB = saasDB;
            _dbSet = _saasDB.Set<T>();
        }
        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        public virtual T GetAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public IAuthenticatedUser CurrentUser
        {
            get
            {
                //in case there is no authentication??
                return _authenticatedUser;
            }
        }
    }
}