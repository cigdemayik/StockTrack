using StockTrack.WebUI.Models.UserProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Models.OpetaionModels
{
    public class ProductPurchaseModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Operation { get; set; } = 2;
    }
}
