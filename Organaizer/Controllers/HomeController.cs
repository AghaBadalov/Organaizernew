using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using Organaizer.Models;
using Organaizer.ViewModels;
using System.Diagnostics;

namespace Organaizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.Where(x => x.IsDeleted == false).ToList(),
                Organaizers=_context.Organaizers.Where(x => x.IsDeleted == false).Include(x=>x.OrganaizerImages).ToList(),
            };
            return View(homeVM);
        }

    }
}