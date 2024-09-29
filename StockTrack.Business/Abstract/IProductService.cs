using StockTrack.Business.Dtos.ProductDtos;
using StockTrack.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Abstract
{
    public interface IProductService
    {
        Task<ServiceResponse<bool>> ChangeStatus(int id);
        Task<ServiceResponse<List<ProductDto>>> GetAll(); 
        Task<ServiceResponse<ProductDto>> GetById(int id);
        Task<ServiceResponse<int>> Create(ProductCreateDto dto);
        Task<ServiceResponse<bool>> Update(ProductUpdateDto dto);
        Task<ServiceResponse<bool>> Delete(int id);
    }
}
