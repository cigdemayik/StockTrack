using Microsoft.EntityFrameworkCore;
using StockTrack.DataAccess.Abstract.Generic;
using StockTrack.DataAccess.Concrete.EfCore.Context;
using StockTrack.Entities.Concrete.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.DataAccess.Concrete.EfCore.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly StoctTrackContext _context;

        public GenericRepository(StoctTrackContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public List<TEntity> AddBulk(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities.ToArray());
            return entities;
        }

        public async Task<bool> AddOperation(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TEntity entity)
        {
            bool value = false;
            try
            {
                _context.Set<TEntity>().Remove(entity);
                value = true;
            }
            catch (Exception)
            {

                throw;
            }
            return value;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            bool value = false;
            try
            {
                await Task.Run(()=>_context.Set<TEntity>().Remove(entity));
                value = true;
            }
            catch (Exception)
            {

                throw;
            }
            return value;
        }

        public IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            var operationData = _context.Set<TEntity>().AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) =>current.Include(include)); 
            }
            if (filter != null)
            {
                operationData = operationData.Where(filter);
            }
            return operationData;
        }

        public async Task<IQueryable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            var operationData = _context.Set<TEntity>().AsTracking(isTracking);
            if (includes !=null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));

            }
            if (filter != null)
            {
                operationData = await Task.Run(() => operationData.Where(filter));
            }
            return operationData;
        }

        public TEntity GetByFilter(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            var operationData = _context.Set<TEntity>().AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));
            }
            return operationData.FirstOrDefault(filter);
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            var operationData = _context.Set<TEntity>().AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));
            }
            return await operationData.FirstOrDefaultAsync(filter);
        }

        public bool Update(TEntity entity)
        {
            bool value = false;
            try
            {
                var updatedEntity = _context.Set<TEntity>().Find(entity.Id);
                _context.Entry(updatedEntity).CurrentValues.SetValues(entity);
                value = true;
            }
            catch (Exception)
            {

            }
            return value;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            bool value = false;
            try
            {
                var updatedEntity = await _context.Set<TEntity>().FindAsync(entity.Id);
                await Task.Run(() => _context.Entry(updatedEntity).CurrentValues.SetValues(entity));
                value = true;
            }
            catch (Exception)
            {

            }
            return value;
        }
    }
}
