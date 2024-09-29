using StockTrack.DataAccess.Abstract.Generic;
using StockTrack.DataAccess.Concrete.EfCore.Context;
using StockTrack.DataAccess.Concrete.EfCore.Repositories.Generic;
using StockTrack.Entities.Concrete.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoctTrackContext _context;

        public UnitOfWork(StoctTrackContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}
