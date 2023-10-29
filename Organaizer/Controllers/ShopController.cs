using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using Organaizer.Models;
using Organaizer.Services;
using Organaizer.ViewModels;
using OrganaizerShop.Models;

namespace Organaizer.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDBContext _context;
        private readonly GetUserService _userService;

        public ShopController(AppDBContext context,GetUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            OrganaizerModel org=_context.Organaizers.Include(x=>x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if (org is null) return View("error");
            return View(org);
        }
        public async Task<IActionResult> AddToBasket(int id)
        {
            if (!_userService.IsUserAuthenticated())
            {
                return RedirectToAction("login", "account");
            }
            var user = await _userService.GetUser();

            var basket = _context.Baskets.FirstOrDefault(x => x.AppUserId == user.Id);
            if (basket is null)
            {
                basket = new Basket
                {
                    AppUserId = user.Id
                };
                _context.Baskets.Add(basket);
            }
            var org = _context.Organaizers.FirstOrDefault(x => x.Id == id);
            if (org is null) return View("error");

            BasketItem basketItem = _context.BasketItems.FirstOrDefault(x => x.OrganaizerModelId == org.Id && x.Basket == basket);
            if (basketItem is null)
            {
                basketItem = new BasketItem
                {
                    Basket = basket,
                    OrganaizerModelId = org.Id,
                    Count = 1

                };
                _context.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Count += 1;
            }
            _context.SaveChanges();



            return RedirectToAction("index","home");
        }
        public async Task<IActionResult> Cart()
        {
            var user = await _userService.GetUser();
            if (user is null)
            {
                return RedirectToAction("login", "account");
            }
        
            
            var cartViewModel = new CartViewModel();
            var basketitems = _context.BasketItems.Where(x => x.Basket.AppUserId == user.Id && x.IsOrdered == false)
                .Select(x => new CartViewModel.BasketItemViewModel
                {
                    Color = x.OrganaizerModel.Color,
                    OrganaizerModelId = x.OrganaizerModelId,
                    OrganaizerName = x.OrganaizerModel.Name,
                    Discount=x.OrganaizerModel.Discount,
                    Price = x.OrganaizerModel.Price,
                    TotalPrice= (x.Count * x.OrganaizerModel.Price).Value,
                    
                    ProductPrice = x.OrganaizerModel.LPrice,
                    ProductCount = x.Count,
                    ImageUrl = x.OrganaizerModel.OrganaizerImages.FirstOrDefault(a => a.OrganaizerModelId == x.OrganaizerModelId).ImageUrl,
                    ProductTotal = (x.Count * x.OrganaizerModel.LPrice).Value,
                    ProductName=x.OrganaizerModel.Name
                    
                }
            ).ToList();
            cartViewModel.BasketItems = basketitems;
            cartViewModel.Total = basketitems.Sum(x => x.ProductPrice * x.ProductCount).Value;
            cartViewModel.LTotal = basketitems.Sum(x => x.Price * x.ProductCount).Value;


            return View(cartViewModel);
            ;        }

    }
}
