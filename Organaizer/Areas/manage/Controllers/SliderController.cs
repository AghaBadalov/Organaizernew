using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Organaizer.DAL;
using Organaizer.Helpers;
using Organaizer.Models;
using System.Net.NetworkInformation;

namespace Organaizer.Areas.manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDBContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Sliders.Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<Slider> sliders=PaginatedList<Slider>.Create(query,6,page);
            return View(sliders);
        }
        public IActionResult DeletedSliders(int page = 1)
        {
            var query = _context.Sliders.Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<Slider> sliders = PaginatedList<Slider>.Create(query, 6, page);
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);
            if(slider.ImageFile==null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil boş saxlanıla bilməz");
                return View(slider);
            }
            if (slider.ImageFile.Length > 2621440)
            {
                ModelState.AddModelError("ImageFile", "Şəkil ölçüsü 2.5 Mb və ya daha az olmalıdır");
                return View(slider);
            }
            if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType !="image/jpeg" )
            {
                ModelState.AddModelError("ImageFile", "Şəkil tipi png, jpeg və ya jpg olmalıdır");
                return View(slider);
            }
            slider.ImageUrl = slider.ImageFile.SaveFile("uploads/sliders", _env.WebRootPath);
            slider.IsDeleted = false;
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if(slider is null) { return View("error"); }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Slider slider)
        {
            Slider exstslider=_context.Sliders.FirstOrDefault(s => s.Id == slider.Id);
            if(exstslider is null) { return View("error"); }
            if (!ModelState.IsValid) return View(slider);
            if(slider.ImageFile is not null)
            {
                if (slider.ImageFile.Length > 2621440)
                {
                    ModelState.AddModelError("ImageFile", "Şəkil ölçüsü 2.5 Mb və ya daha az olmalıdır");
                    return View(slider);
                }
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Şəkil tipi png, jpeg və ya jpg olmalıdır");
                    return View(slider);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", exstslider.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstslider.ImageUrl = slider.ImageFile.SaveFile("uploads/sliders",_env.WebRootPath);
            }
            exstslider.Desc = slider.Desc;
            exstslider.Name = slider.Name;
            exstslider.Card = slider.Card;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null) { return View("error"); }
            slider.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Repair(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null) { return View("error"); }
            slider.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deletedsliders");
        }
        public IActionResult HardDelete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null) { return View("error"); }
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("deletedsliders");
        }
    }
}
