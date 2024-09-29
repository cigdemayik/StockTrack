using Microsoft.EntityFrameworkCore;
using StockTrack.Entities.Concrete;
using StockTrack.Entities.Concrete.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.DataAccess.Abstract.Generic
{
    public interface IGenericRepository<TEntity> where TEntity:BaseEntity
    {
        #region Syncronics
        IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        TEntity GetByFilter(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        TEntity Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        List<TEntity> AddBulk(List<TEntity> entities);
        #endregion

        #region Asyncronics
        Task<IQueryable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> AddOperation(TEntity entity);
        #endregion
    }
}
