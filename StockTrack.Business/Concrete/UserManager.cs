using Mapster;
using StockTrack.Business.Abstract;
using StockTrack.Business.Dtos.UserDtos;
using StockTrack.DataAccess;
using StockTrack.Entities.Concrete;
using StockTrack.Helpers.Abstract;
using StockTrack.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public UserManager(IServiceResponseHelper serviceResponseHelper, IUnitOfWork unitOfWork)
        {
            _serviceResponseHelper = serviceResponseHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<int>> Create(UserCreateDto dto)
        {
            try
            {
                var mappedData = dto.Adapt<User>();
                var result = await _unitOfWork.GetRepository<User>().AddAsync(mappedData);
                await _unitOfWork.SaveChangesAsync();
                if (result != null)
                    return _serviceResponseHelper.SetSuccess<int>(result.Id, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<int>(-1, "User ekleme işlemi başarısız", System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return _serviceResponseHelper.SetError<int>(0, "User Ekleme sırasında bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int id)
        {
            try
            {
                var data = await _unitOfWork.GetRepository<User>().GetByFilterAsync(x => x.Id == id, null, Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking);
                var operation = await _unitOfWork.GetRepository<User>().DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                if (operation)
                    return _serviceResponseHelper.SetSuccess<bool>(true, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<bool>(false, "User silinemedi", System.Net.HttpStatusCode.BadRequest);

            }
            catch (Exception)
            {

                return _serviceResponseHelper.SetError<bool>(false, "User silinirken bir hata ile karşılaşıldı", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResponse<User>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<UserLoginResponseDto> SignIn(UserLoginDto dto)
        {
            var user = _unitOfWork.GetRepository<User>().GetByFilter(x => x.UserName == dto.UserName && x.Password == dto.Password);
            var mappedData = user.Adapt<UserLoginResponseDto>();
            if (user != null)
            {


                return _serviceResponseHelper.SetSuccess(mappedData, HttpStatusCode.Created);

            }
            return _serviceResponseHelper.SetError<UserLoginResponseDto>(null, "Kullanıcı Adı ve Şifre Eşleşmemektedir"
                , HttpStatusCode.NotFound);
        }

        public async Task<ServiceResponse<bool>> Update(UserUpdateDto dto)
        {
            try
            {
                var mappedData = dto.Adapt<User>();
                var data = await _unitOfWork.GetRepository<User>().UpdateAsync(mappedData);
                await _unitOfWork.SaveChangesAsync();
                if (data)
                    return _serviceResponseHelper.SetSuccess<bool>(data, System.Net.HttpStatusCode.OK);
                return _serviceResponseHelper.SetError<bool>(data, "User güncelleme işlemi yapılamadı", System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {

                return _serviceResponseHelper.SetError<bool>(false, "User güncellenirken bir sorun ile karşılaşıldı.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
