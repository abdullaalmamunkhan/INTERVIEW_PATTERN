using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Web_DAL.IRepository;
using Web_Entity;

namespace Web_DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {
        protected readonly WebContext _context;

        public GenericRepository(WebContext context) {
            context = _context;
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public IQueryable<TEntity> GetALL(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }


        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }
        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public EntityEntry<TEntity> Insert2(TEntity entity)
        {
            var result = _context.Set<TEntity>().Add(entity);
            return result;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        public void Delete(object id)
        {
            TEntity existing = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(existing);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
