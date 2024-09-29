using StockTrack.Business.Dtos.UserDtos;
using StockTrack.Entities.Concrete;
using StockTrack.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Business.Abstract
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> GetById(int id);
        ServiceResponse<UserLoginResponseDto> SignIn(UserLoginDto dto);
        Task<ServiceResponse<int>> Create(UserCreateDto dto);
        Task<ServiceResponse<bool>> Update(UserUpdateDto dto);
        Task<ServiceResponse<bool>> Delete(int id);
    }
}
