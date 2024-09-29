using StockTrack.WebUI.Models.UserProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Models.OpetaionModels
{
    public class ProductSaleModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Miktar")]
        [Required(ErrorMessage ="Stokta yeterince ürününüz yok.")]
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Operation { get; set; } = 1;
    }
}
