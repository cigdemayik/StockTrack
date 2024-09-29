using StockTrack.Business.Dtos.ProductDtos;
using StockTrack.Business.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Dtos.UserProductDtos
{
    public class UserProductDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserLoginResponseDto User { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
