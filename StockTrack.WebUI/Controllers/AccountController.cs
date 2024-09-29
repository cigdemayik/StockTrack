using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTrack.Business.Abstract;
using StockTrack.Business.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new UserLoginDto());
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserLoginDto dto)
        {
            var response = _userService.SignIn(dto);
            if (!response.IsSuccessful)
            {
                ViewBag.Message = response.ErrorMessage;
                return View(dto);
            }

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, response.Result.Id.ToString()),
                    new Claim(ClaimTypes.Name, response.Result.UserName.ToString())
                };


            var identity = new ClaimsIdentity(claims, "Custom Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { identity });
            
            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("WebSite.Cookie");
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Account");
        }
    }
}
