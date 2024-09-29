using StockTrack.WebUI.Models.UserProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Models.UserProductHistoryModels
{
    public class UserProductHistoryModel
    {
        public int Id { get; set; }
        public int Operation { get; set; }
        public DateTime OperationDate { get; set; }
        public int Price { get; set; }
        public int UserProductId { get; set; }
        public UserProductModel UserProduct { get; set; }
        public int Quantity { get; set; }
    }
}
