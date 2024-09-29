using Microsoft.AspNetCore.Mvc;
using StockTrack.WebUI.Models.CookieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Extentions
{
    public static class ControllerExtention
    {
        public static UserCookieModel GetUserInfo(this Controller controller)
        {
            UserCookieModel model = new UserCookieModel();
            model.UserId = Convert.ToInt32(controller.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            model.UserName = controller.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            return model;
        }
    }
}
