using Mapster;
using StockTrack.Business.Abstract;
using StockTrack.Business.Dtos.UserProductDtos;
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
    public class UserProductManager : IUserProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public UserProductManager(IServiceResponseHelper serviceResponseHelper, IUnitOfWork unitOfWork)
        {
            _serviceResponseHelper = serviceResponseHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> ChangeStatus(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<UserProduct>().GetByFilterAsync(x => x.Id == id);
                data.Active = !data.Active;
                await _unitOfWork.SaveChangesAsync();
                return _serviceResponseHelper.SetSuccess<bool>(true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<bool>(false, "UserProduct durumu değiştiriliyorken sorunla karşılaşıldı", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<List<UserProductDto>>> GetAll()
        {

            try
            {
                var includes = new List<Expression<Func<UserProduct, object>>>();
                includes.Add(x => x.Product);
                var data = await _unitOfWork.GetRepository<UserProduct>().GetAllByFilterAsync();
                var mappedData = data.ToList().Adapt<List<UserProductDto>>();
                if (mappedData != null)
                    return _serviceResponseHelper.SetSuccess<List<UserProductDto>>(mappedData, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<List<UserProductDto>>(null, "Hiç UserProduct kaydı bulunamadı", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<List<UserProductDto>>(null, "UserProduct Getirme sırasında bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<UserProductDto>> GetById(int id)
        {
            try
            {
                var includes = new List<Expression<Func<UserProduct, object>>>();
                includes.Add(x => x.Product);
                var data = await _unitOfWork.GetRepository<UserProduct>().GetByFilterAsync(x => x.Id == id, includes.ToArray());
                var dto = data.Adapt<UserProductDto>();
                if (dto != null)
                {
                    return _serviceResponseHelper.SetSuccess<UserProductDto>(dto, System.Net.HttpStatusCode.OK);
                }
                return _serviceResponseHelper.SetError<UserProductDto>(null, "StokTa Böyle Bir Ürününüz Bulunmamaktadır", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<UserProductDto>(null, "Ürün Getirilirken Bir Sorun İle Karşılaşıldı", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<ServiceResponse<List<UserProductDto>>> GetProductsWithUserId(int userId)
        {
            try
            {
                var includes = new List<Expression<Func<UserProduct, object>>>();
                includes.Add(x=>x.Product);
                var data = await _unitOfWork.GetRepository<UserProduct>().GetAllByFilterAsync(x=>x.UserId== userId, includes.ToArray());
                var mappedData = data.ToList().Adapt<List<UserProductDto>>();
                if (mappedData != null)
                    return _serviceResponseHelper.SetSuccess<List<UserProductDto>>(mappedData, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<List<UserProductDto>>(null, "Hiç UserProduct kaydı bulunamadı", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {

                return _serviceResponseHelper.SetError<List<UserProductDto>>(null, "UserProduct kayıtları getirilirken bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
