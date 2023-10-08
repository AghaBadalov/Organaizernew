using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using Organaizer.Helpers;
using Organaizer.Models;
using OrganaizerShop.Models;

namespace Organaizer.Areas.manage.Controllers
{
    [Area("manage")]
    public class OrganaizerModelController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public OrganaizerModelController(AppDBContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        
        public IActionResult Index(int page=1)
        {
            var query = _context.Organaizers.Include(x=>x.OrganaizerImages).Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<OrganaizerModel> organaizers = PaginatedList<OrganaizerModel>.Create(query, 6, page);
            return View(organaizers);
        }
        public IActionResult Details(int id)
        {
            OrganaizerModel org = _context.Organaizers.Include(x => x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if(org is null) { return View("error"); }
            return View(org);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OrganaizerModel org)
        {
            if (!ModelState.IsValid)
            {
                return View(org);
            }
            if(org.PosterImage is null)
            {
                ModelState.AddModelError("PosterImage", "Boş saxlanıla bilməz");
                return View(org);
            }
            if(org.PosterImage.ContentType!="image/jpeg" && org.PosterImage.ContentType != "image/png")
            {
                ModelState.AddModelError("PosterImage", "Şəkil tipi png, jpeg və ya jpg olmalıdır");
                return View(org);
            }
            if(org.PosterImage.Length> 2621440)
            {
                ModelState.AddModelError("PosterImage", "Şəkil ölçüsü 2.5 Mb və ya daha az olmalıdır");
                return View(org);
            }
            OrganaizerImages image = new OrganaizerImages
            {
                ImageUrl = org.PosterImage.SaveFile("uploads/organaizers", _env.WebRootPath),
                IsPoster = true,
                OrganaizerModel = org

            };
            _context.OrganaizerImages.Add(image);
            if (org.Images is not null)
            {
                if (org.Images.Count > 4)
                {
                    ModelState.AddModelError("Images", "Şəkillərin sayı 4dən çox ola bilməz");
                    return View(org);
                }
                foreach (var img in org.Images)
                {
                    if (img.ContentType != "image/jpeg" && img.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Images", "Şəkil tipi png, jpeg və ya jpg olmalıdır");
                        return View(org);
                    }
                    if (img.Length > 2621440)
                    {
                        ModelState.AddModelError("Images", "Şəkil ölçüsü 2.5 Mb və ya daha az olmalıdır");
                        return View(org);
                    }
                    OrganaizerImages orgimages = new OrganaizerImages
                    {
                        ImageUrl = img.SaveFile("uploads/organaizers", _env.WebRootPath),
                        IsPoster = false,
                        OrganaizerModel = org

                    };
                    _context.OrganaizerImages.Add(orgimages);

                }
            }
            
            org.IsDeleted = false;
            _context.Organaizers.Add(org);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            OrganaizerModel org = _context.Organaizers.Include(x=>x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if (org is null) return View("error");
            return View(org);
        }
        [HttpPost]
        public  IActionResult Update(OrganaizerModel org)
        {
            if (!ModelState.IsValid) return View(org);
            OrganaizerModel exorg = _context.Organaizers.Include(x => x.OrganaizerImages).FirstOrDefault(x => x.Id == org.Id);
            if(exorg is null) return View("error");
            if (org.OrgImagesIds is null)
            {

                List<OrganaizerImages> ImagesDelet = exorg.OrganaizerImages.Where(ai => ai.OrganaizerModelId == exorg.Id).Where(ai => ai.IsPoster is false).ToList();
                foreach (var item in ImagesDelet)
                {
                    string path = Path.Combine(_env.WebRootPath, "uploads/organaizers", item.ImageUrl);
                    if (path != null)
                    {
                        System.IO.File.Delete(path);
                    }
                }

                exorg.OrganaizerImages.RemoveAll(x => x.OrganaizerModelId == exorg.Id && x.IsPoster is false);

            }
            else
            {
                List<OrganaizerImages> ImagesDelet = exorg.OrganaizerImages.FindAll(ai => !org.OrgImagesIds.Contains(ai.Id) && ai.IsPoster is false);
                foreach (var item in ImagesDelet)
                {
                    string path = Path.Combine(_env.WebRootPath, "uploads/organaizers", item.ImageUrl);

                    if (path!=null)
                    {
                        System.IO.File.Delete(path);
                    }
                }

                exorg.OrganaizerImages.RemoveAll(ai => !org.OrgImagesIds.Contains(ai.Id) && ai.IsPoster is false);
            }
            if (org.PosterImage != null)
            {
                if (org.PosterImage.ContentType != "image/jpeg" && org.PosterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("PosterImage", "Şəkil tipi png, jpeg və ya jpg olmalıdır");
                    return View(org);
                }
                if (org.PosterImage.Length > 2621440)
                {
                    ModelState.AddModelError("PosterImage", "Şəkil ölçüsü 2.5 Mb və ya daha az olmalıdır");
                    return View(org);
                }
                foreach (var item in exorg.OrganaizerImages)
                {
                    if (item.IsPoster is true)
                    {
                        string path = Path.Combine(_env.WebRootPath, "uploads/organaizers", item.ImageUrl);
                        if (path != null)
                        {
                            System.IO.File.Delete(path);
                        };

                        _context.Remove(item);
                    }
                }
                OrganaizerImages Images = new OrganaizerImages
                {
                    ImageUrl = org.PosterImage.SaveFile("uploads/organaizers", _env.WebRootPath),
                    IsPoster = true,
                    OrganaizerModelId = org.Id

                };
                _context.OrganaizerImages.Add(Images);

            }

            

            if (org.Images !=null)
            {
                if (org.Images.Count > 4)
                {
                    ModelState.AddModelError("Images", "Şəkillərin sayı 4dən çox ola bilməz");
                    return View(org);
                }
                foreach (var image in org.Images)
                {
                    if (image.ContentType != "image/jpeg" && image.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Images", "Şəkil tipi png, jpeg və ya jpg olmalıdır");
                        return View(org);
                    }
                    if (image.Length > 2621440)
                    {
                        ModelState.AddModelError("Images", "Şəkil ölçüsü 2.5 Mb və ya daha az olmalıdır");
                        return View(org);
                    }
                    OrganaizerImages orgimages = new OrganaizerImages
                    {
                        OrganaizerModelId = org.Id,
                        ImageUrl = image.SaveFile("uploads/organaizers", _env.WebRootPath),
                        IsPoster = false
                    };
                   _context.OrganaizerImages.Add(orgimages);

                }
            }
            
            exorg.Purpose = org.Purpose;
            exorg.Count = org.Count;
            exorg.Color = org.Color;
            exorg.Discount = org.Discount;
            exorg.Price = org.Price;
            exorg.Desc = org.Desc;
            exorg.Type = org.Type;
            exorg.Name = org.Name;
            _context.SaveChanges();




            return RedirectToAction("index");

        }
        public IActionResult Deactive(int id)
        {
            OrganaizerModel OrgM = _context.Organaizers.Include(x => x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if (OrgM is null) return View("error");
            OrgM.IsDeleted = true;
            _context.SaveChanges();
           return   RedirectToAction("index");
        }
        public IActionResult ReActive(int id)
        {
            OrganaizerModel OrgM = _context.Organaizers.Include(x => x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if (OrgM is null) return View("error");
            OrgM.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult DeActives(int page=1)
        {
            var query = _context.Organaizers.Include(x => x.OrganaizerImages).Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<OrganaizerModel> organaizers = PaginatedList<OrganaizerModel>.Create(query, 6, page);
            return View(organaizers);

        }
        public IActionResult Delete(int id)
        {
            OrganaizerModel model = _context.Organaizers.Include(x => x.OrganaizerImages).FirstOrDefault(x => x.Id == id);
            if (model is null) return View("error");
            foreach(var item in model.OrganaizerImages)
            {
                string path = Path.Combine(_env.WebRootPath, "uploads/organaizers", item.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _context.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("DeActives");
        }
    }
}
