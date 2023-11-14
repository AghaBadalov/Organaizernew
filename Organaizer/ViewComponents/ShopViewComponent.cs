using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using OrganaizerShop.Models;

namespace Organaizer.ViewComponents
{
    public class ShopViewComponent :ViewComponent
    {
        private readonly AppDBContext _context;

        public ShopViewComponent(AppDBContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<OrganaizerModel> abouts = _context.Organaizers.Where(x=>x.Discount>0).Include(x=>x.OrganaizerImages).ToList();
            return View(abouts);
        }
    }
}
