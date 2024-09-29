using Mapster;
using StockTrack.Business.Abstract;
using StockTrack.Business.Dtos.ProductDtos;
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
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public ProductManager(IServiceResponseHelper serviceResponseHelper, IUnitOfWork unitOfWork)
        {
            _serviceResponseHelper = serviceResponseHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> ChangeStatus(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<Product>().GetByFilterAsync(x => x.Id == id);
                data.Active = !data.Active;
                await _unitOfWork.SaveChangesAsync();
                return _serviceResponseHelper.SetSuccess<bool>(true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<bool>(false, "Product durumu değiştiriliyorken sorunla karşılaşıldı", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<int>> Create(ProductCreateDto dto)
        {
            try
            {
                var mappedData = dto.Adapt<Product>();
                var result = await _unitOfWork.GetRepository<Product>().AddAsync(mappedData);
                await _unitOfWork.SaveChangesAsync();
                if (result != null)
                    return _serviceResponseHelper.SetSuccess<int>(result.Id, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<int>(-1, "Product ekleme işlemi başarısız", System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return _serviceResponseHelper.SetError<int>(0, "Product Ekleme sırasında bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<Product>().GetByFilterAsync(x => x.Id == id, null, Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking);
                var operation = await _unitOfWork.GetRepository<Product>().DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                if (operation)
                    return _serviceResponseHelper.SetSuccess<bool>(true, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<bool>(false, "Product silinemedi", System.Net.HttpStatusCode.BadRequest);

            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<bool>(false, "Product silinirken bir hata ile karşılaşıldı", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAll()
        {
            try
            {
                var data = await _unitOfWork.GetRepository<Product>().GetAllByFilterAsync();
                var mappedData = data.ToList().Adapt<List<ProductDto>>();
                if (mappedData != null)
                    return _serviceResponseHelper.SetSuccess<List<ProductDto>>(mappedData, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<List<ProductDto>>(null, "Hiç Product kaydı bulunamadı", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<List<ProductDto>>(null, "Product Getirme sırasında bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<ProductDto>> GetById(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<Product>().GetAllByFilterAsync(x => x.Id == id);
                var dto = data.Adapt<ProductDto>();
                if (dto != null)
                {
                    return _serviceResponseHelper.SetSuccess<ProductDto>(dto, System.Net.HttpStatusCode.OK);
                }
                return _serviceResponseHelper.SetError<ProductDto>(null, "Böyle Bir Ürününüz Bulunmamaktadır", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<ProductDto>(null, "Ürün Getirilirken Bir Sorun İle KArşılaşıldı", System.Net.HttpStatusCode.BadRequest);
            }
        }


        public async Task<ServiceResponse<bool>> Update(ProductUpdateDto dto)
        {
            try
            {
                var mappedData = dto.Adapt<Product>();
                var data = await _unitOfWork.GetRepository<Product>().UpdateAsync(mappedData);
                await _unitOfWork.SaveChangesAsync();
                if (data)
                    return _serviceResponseHelper.SetSuccess<bool>(data, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<bool>(data, "Product güncelleme işlemi yapılamadı", System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {

                return _serviceResponseHelper.SetError<bool>(false, "Product güncellenirken bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
