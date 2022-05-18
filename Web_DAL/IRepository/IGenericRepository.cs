using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Web_DAL.IRepository
{
   public interface IGenericRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetALL(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(object id);
        void Insert(TEntity entity);
        EntityEntry<TEntity> Insert2(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Save();
    }
}
