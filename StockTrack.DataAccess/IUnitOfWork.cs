using StockTrack.DataAccess.Abstract.Generic;
using StockTrack.Entities.Concrete.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.DataAccess
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
        void SaveChanges();
        Task SaveChangesAsync();
    }

}
