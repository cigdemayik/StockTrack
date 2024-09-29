using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockTrack.Business.Abstract;
using StockTrack.Business.Dtos.ProductDtos;
using StockTrack.Business.Dtos.UserDtos;
using StockTrack.Business.Dtos.UserProductHistoryDtos;
using StockTrack.WebUI.Extentions;
using StockTrack.WebUI.Models.OpetaionModels;
using StockTrack.WebUI.Models.ProductModels;
using StockTrack.WebUI.Models.UserModels;
using StockTrack.WebUI.Models.UserProductHistoryModels;
using StockTrack.WebUI.Models.UserProductModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrack.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserProductService _userProductService;
        private readonly IProductService _productService;
        private readonly IUserProductHistoryService _userProductHistoryService;
        private readonly IUserService _userService;

        public HomeController(IUserProductService userProductService, IProductService productService, IUserProductHistoryService userProductHistoryService, IUserService userService)
        {
            _userProductService = userProductService;
            _productService = productService;
            _userProductHistoryService = userProductHistoryService;
            _userService = userService;
        }
        public IActionResult Index()
        {

            return View();
        }
        #region UserProduct
        public async Task<IActionResult> UserProduct()
        {
            var userInfo = this.GetUserInfo();
            var response = await _userProductService.GetProductsWithUserId(userInfo.UserId);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return View(response.Result.Adapt<List<UserProductModel>>());
        }
        #endregion

        #region UserProdutcHistory

        public async Task<IActionResult> UserProductHistory()
        {
            var userInfo = this.GetUserInfo();
            var response = await _userProductHistoryService.GetAllByUserId(userInfo.UserId);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return View(response.Result.Adapt<List<UserProductHistoryModel>>());
        }
        #endregion

        #region Operations
        public async Task<IActionResult> ProductSale(int id)
        {
            var response = await _userProductService.GetById(id);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return View(response.Result.Adapt<ProductSaleModel>());
        }
        [HttpPost]
        public async Task<IActionResult> ProductSale(ProductSaleModel model)
        {
            var quantity = _userProductService.GetById(model.Id);
            if(quantity.Result.Result.Quantity > model.Quantity) 
            { 
            if (ModelState.IsValid)
            {
                var mappedData = model.Adapt<UserProductSaleDto>();
                var response = await _userProductHistoryService.ProductSale(mappedData);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("NotFound");
                }
                return RedirectToAction("UserProduct");
            }
            return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> ProductPurchase(int id)
        {
            var model = new ProductPurchaseModel();
            model.ProductId = id;
            var product = await _productService.GetById(id);
            if (product.IsSuccessful)
            {
                ViewData["ProductName"] = product.Result.Name;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductPurchase(ProductPurchaseModel model)
        {
            var userInfo = this.GetUserInfo();
            var mappedData = model.Adapt<UserProductPurchaseDto>();
            mappedData.UserId = userInfo.UserId;
            var response = await _userProductHistoryService.ProductPurchase(mappedData);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return RedirectToAction("UserProduct");
        }
        #endregion
        #region Product

        public async Task<IActionResult> AdminProduct()
        {
            var response = await _productService.GetAll();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return View(response.Result.Adapt<List<ProductModel>>());
        }
        public async Task<IActionResult> Product()
        {
            var response = await _productService.GetAll();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return View(response.Result.Adapt<List<ProductModel>>());
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View(new ProductCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductCreateModel model)
        {
            var mappeddata = model.Adapt<ProductCreateDto>();
            var response = await _productService.Create(mappeddata);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return View("NotFound");
            }
            return RedirectToAction("AdminProduct");
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var response = await _productService.GetById(id);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return View(response.Result.Adapt<ProductUpdateModel>());
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductUpdateModel model)
        {
            var mappedData = model.Adapt<ProductUpdateDto>();
            var response = await _productService.Update(mappedData);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return RedirectToAction("AdminProduct");
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            var response = await _productService.Delete(id);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return RedirectToAction("NotFound");

            return RedirectToAction("AdminProduct");
        }

        #endregion

        #region Users

        public async Task<IActionResult> UserCreate()
        {
            return View(new UserCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(UserCreateModel model)
        {
            var mappeddata = model.Adapt<UserCreateDto>();
            var response = await _userService.Create(mappeddata);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NotFound");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserDelete(int id)
        {
            var response = await _userService.Delete(id);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return RedirectToAction("NotFound");

            return RedirectToAction("User");
        }
        #endregion
    }
}
