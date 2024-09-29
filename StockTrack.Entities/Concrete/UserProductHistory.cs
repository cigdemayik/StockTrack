using StockTrack.Entities.Abstract;
using StockTrack.Entities.Concrete.BaseModel;
using StockTrack.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Entities.Concrete
{
    public class UserProductHistory:BaseEntity, ITable
    {
        public int Operation { get; set; }
        public DateTime OperationDate { get; set; }
        public int Price { get; set; }
        public int UserProductId { get; set; }
        public UserProduct UserProduct { get; set; }
        public int Quantity { get; set; }
    }
}
