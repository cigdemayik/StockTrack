using StockTrack.Entities.Abstract;
using StockTrack.Entities.Concrete.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Entities.Concrete
{
    public class User:BaseEntity, ITable
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
    }
}
