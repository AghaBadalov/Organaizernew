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
            OrganaizerModel Org = _context.Organaizers.Include(x=>x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if (Org is null) return View("Error");
            AppUser user = null;
            if (User.Identity.IsAuthenticated is true)
            {
                user = await _userService.GetUser();
            }
            else
            {
                return RedirectToAction("login", "account");

            }
            BasketItem basketItem = new BasketItem
            {
                
            };
            
            //Basket basket = _context.Baskets.Where(x=>x.IsActive==true).FirstOrDefault(x => x.AppUserId == user.Id);
            //if (basket != null)
            //{

            //    basket.BasketItems.Add(basketItem);
            //}
            //else
            //{
            //    Basket newbasket = new Basket
            //    {
            //        IsActive = true,
            //        AppUserId = user.Id,
                    
            //    };
            //    newbasket.BasketItems.Add(basketItem);

            //}

            //List<BasketItem> basketItems = new List<BasketItem>();
            //BasketItem basketItem = null;
            //if(Org is not null && Org.Count > 0)
            //{
            //    basketItem = new BasketItem
            //    {
            //        OrganaizerModelId = id,
            //        Count = 1,
                    

            //    };
            //    basketItems.Add(basketItem);
            //}

            return RedirectToAction("index","home");
        }
        public async Task<IActionResult> Cart()
        
        {
            //Basket basket = new Basket();
            AppUser user = null;
            if (User.Identity.IsAuthenticated is true)
            {
                user = await _userService.GetUser();
            }
            else
            {
                return RedirectToAction("login","account");
            }
            //if(user is not null)
            //{
            //    basket.AppUserId = user.Id;
            //}
            

            return View()
;        }

    }
}
