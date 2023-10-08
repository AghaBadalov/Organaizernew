using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using OrganaizerShop.Models;

namespace Organaizer.ViewComponents
{
    public class OrganaizerViewComponent: ViewComponent
    {
        private readonly AppDBContext _context;

        public OrganaizerViewComponent(AppDBContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<OrganaizerModel> abouts = _context.Organaizers.Include(x=>x.OrganaizerImages).ToList();
            return View(abouts);
        }
    }
}
