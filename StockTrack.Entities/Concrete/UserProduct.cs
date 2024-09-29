using StockTrack.Entities.Abstract;
using StockTrack.Entities.Concrete.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Entities.Concrete
{
    public class UserProduct : BaseEntity,ITable
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public ICollection<UserProductHistory> UserProductHistories { get; set; }
    }
}
