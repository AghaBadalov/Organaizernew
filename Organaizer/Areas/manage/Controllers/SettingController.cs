using Microsoft.AspNetCore.Mvc;
using Organaizer.DAL;
using Organaizer.Helpers;
using Organaizer.Models;

namespace Organaizer.Areas.manage.Controllers
{
    [Area("manage")]
    public class SettingController : Controller
    {
        private readonly AppDBContext _context;

        public SettingController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Settings.AsQueryable();
            PaginatedList<Setting> settings = PaginatedList<Setting>.Create(query, 6, page);

            return View(settings);
        }
        public IActionResult Update(int id)
        {
            
            Setting setting=_context.Settings.FirstOrDefault(x=>x.Id==id);
            if (setting is null) return View("error");
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Setting setting)
        {
            Setting exstsetting=_context.Settings.FirstOrDefault(x=>x.Id==setting.Id);
            if(exstsetting is null) return View("error");
            if (!ModelState.IsValid) return View(setting);
            if(setting.Value is null)
            {
                ModelState.AddModelError("Value", "Boş qoyula bilmez");
                return View(setting);
            }
            exstsetting.Value=setting.Value;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
