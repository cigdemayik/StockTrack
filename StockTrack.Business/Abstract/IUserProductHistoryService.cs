using StockTrack.Business.Dtos.UserProductHistoryDtos;
using StockTrack.Helpers.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrack.Business.Abstract
{
    public interface IUserProductHistoryService
    {
        Task<ServiceResponse<bool>> ChangeStatus(int id);
        Task<ServiceResponse<int>> ProductPurchase(UserProductPurchaseDto dto);
        Task<ServiceResponse<int>> ProductSale(UserProductSaleDto dto);
        Task<ServiceResponse<List<UserProductHistoryDto>>> GetAllByUserId(int userId);
        Task<ServiceResponse<UserProductHistoryDto>> GetById(int id);
    }
}
