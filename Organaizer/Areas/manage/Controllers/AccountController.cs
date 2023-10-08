using Microsoft.AspNetCore.Mvc;

namespace Organaizer.Areas.manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]


    }
}
