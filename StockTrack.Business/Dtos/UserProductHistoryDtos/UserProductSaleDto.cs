using StockTrack.Business.Dtos.UserProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Dtos.UserProductHistoryDtos
{
    public class UserProductSaleDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Operation { get; set; } = 1;
    }
}
