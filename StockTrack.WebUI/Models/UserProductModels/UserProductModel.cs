using StockTrack.Business.Dtos.ProductDtos;
using StockTrack.WebUI.Models.ProductModels;
using StockTrack.WebUI.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Models.UserProductModels
{
    public class UserProductModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserLoginModel User { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
