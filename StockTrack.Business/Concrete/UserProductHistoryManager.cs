using Mapster;
using StockTrack.Business.Abstract;
using StockTrack.Business.Dtos.ProductDtos;
using StockTrack.Business.Dtos.UserProductDtos;
using StockTrack.Business.Dtos.UserProductHistoryDtos;
using StockTrack.DataAccess;
using StockTrack.Entities.Concrete;
using StockTrack.Helpers.Abstract;
using StockTrack.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Concrete
{
    public class UserProductHistoryManager : IUserProductHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public UserProductHistoryManager(IServiceResponseHelper serviceResponseHelper, IUnitOfWork unitOfWork)
        {
            _serviceResponseHelper = serviceResponseHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> ChangeStatus(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<UserProductHistory>().GetByFilterAsync(x => x.Id == id);
                data.Active = !data.Active;
                await _unitOfWork.SaveChangesAsync();
                return _serviceResponseHelper.SetSuccess<bool>(true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<bool>(false, "Stok durumu değiştiriliyorken sorunla karşılaşıldı", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<List<UserProductHistoryDto>>> GetAllByUserId(int userId)
        {
            try
            {
                var includes = new List<Expression<Func<UserProductHistory, object>>>();
                includes.Add(x => x.UserProduct.Product);
                var data = await _unitOfWork.GetRepository<UserProductHistory>().GetAllByFilterAsync(x => x.UserProduct.UserId == userId,
                     includes.ToArray());
                var dto = data.ToList().Adapt<List<UserProductHistoryDto>>();
                if (dto != null)
                    return _serviceResponseHelper.SetSuccess<List<UserProductHistoryDto>>(dto, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<List<UserProductHistoryDto>>(null, "Hiç UserProductHistory kaydı bulunamadı", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {

                return _serviceResponseHelper.SetError<List<UserProductHistoryDto>>(null, "UserProductHistory kayıtları getirilirken bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<UserProductHistoryDto>> GetById(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<UserProductHistory>().GetByFilterAsync(x => x.Id == id);
                var dto = data.Adapt<UserProductHistoryDto>();
                if (dto != null)
                {
                    return _serviceResponseHelper.SetSuccess<UserProductHistoryDto>(dto, System.Net.HttpStatusCode.OK);
                }
                return _serviceResponseHelper.SetError<UserProductHistoryDto>(null, "Böyle Bir İşleminiz Bulunmamaktadır", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<UserProductHistoryDto>(null, "İşlemleriniz Getirilirken Bir Sorun İle Karşılaşıldı", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<ServiceResponse<int>> ProductPurchase(UserProductPurchaseDto dto)
        {
            try
            {
                //bu ürün ilk defa mı satın alınıyor?
                var userProduct = _unitOfWork.GetRepository<UserProduct>().GetByFilter(x => x.ProductId == dto.ProductId && x.UserId==dto.UserId,
                    isTracking:Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll);
                if (userProduct != null)
                {
                    var userProductHistory = new UserProductHistory()
                    {
                        Operation = 1,
                        Quantity = dto.Quantity,
                        Price = dto.Price,
                        UserProductId = userProduct.Id,
                        Active = true,
                        
                    };
                    var history = await _unitOfWork.GetRepository<UserProductHistory>().AddAsync(userProductHistory);
                    userProduct.Quantity = userProduct.Quantity + dto.Quantity;
                    userProduct.TotalPrice = userProduct.TotalPrice + (dto.Price* dto.Quantity);
                    await _unitOfWork.SaveChangesAsync();
                    return _serviceResponseHelper.SetSuccess<int>(1, System.Net.HttpStatusCode.OK);
                }
                else
                {
                    var userProductEntity = new UserProduct()
                    {
                        ProductId=dto.ProductId, 
                        Quantity=dto.Quantity,
                        TotalPrice=dto.Price*dto.Quantity,
                        UserId=dto.UserId,
                        Active=true,
                        
                    };
                    var newUserProduct = await _unitOfWork.GetRepository<UserProduct>().AddAsync(userProductEntity);
                    await _unitOfWork.SaveChangesAsync();
                    var userProductHistoryEntity = new UserProductHistory()
                    {
                        Operation = 1,
                        Quantity = dto.Quantity,
                        Price = dto.Price,
                        UserProductId = newUserProduct.Id,
                        Active = true,
                    };
                    var userProductHistory = await _unitOfWork.GetRepository<UserProductHistory>().AddAsync(userProductHistoryEntity);
                    await _unitOfWork.SaveChangesAsync();
                    return _serviceResponseHelper.SetSuccess<int>(1, System.Net.HttpStatusCode.OK);
                }
                return _serviceResponseHelper.SetError<int>(0, "Ürün Satın Alınamadı", System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {

                return _serviceResponseHelper.SetError<int>(-1, "Ürün Satın Alma işlemi başarısız", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<ServiceResponse<int>> ProductSale(UserProductSaleDto dto)
        {
            try
            {
                var data = _unitOfWork.GetRepository<UserProduct>().GetByFilter(x => x.Id == dto.Id, isTracking: Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll);
                var stockQuantity = data.Quantity;
                if (data != null)
                {
                    if (stockQuantity >= dto.Quantity)
                    {
                        var userProductHistory = new UserProductHistory()
                        {
                            Operation = 1,
                            Quantity = dto.Quantity,
                            Price = dto.Price,
                            UserProductId = data.Id,
                            Active = true,

                        };
                        var history = await _unitOfWork.GetRepository<UserProductHistory>().AddAsync(userProductHistory);
                        data.Quantity = data.Quantity - dto.Quantity;
                        data.TotalPrice = data.TotalPrice + (dto.Price * dto.Quantity);
                        await _unitOfWork.SaveChangesAsync();
                        return _serviceResponseHelper.SetSuccess<int>(1, System.Net.HttpStatusCode.OK);
                    }
                    return _serviceResponseHelper.SetError<int>(-2, "Stok Yetersiz", System.Net.HttpStatusCode.NotFound);
                }
                return _serviceResponseHelper.SetError<int>(-1, "Satış işleminiz başarılı olmadı",System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<int>(0, "Satış İşleminizde bir soru ile karşılaşıldı", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
