using StockTrack.Business.Dtos.UserProductDtos;
using StockTrack.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Abstract
{
    public interface IUserProductService
    {
        Task<ServiceResponse<bool>> ChangeStatus(int id);
        Task<ServiceResponse<List<UserProductDto>>> GetAll();
        Task<ServiceResponse<List<UserProductDto>>> GetProductsWithUserId(int userId);
        Task<ServiceResponse<UserProductDto>> GetById(int id);
    }
}
