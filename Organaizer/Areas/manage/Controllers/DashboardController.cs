using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Organaizer.Models;

namespace Organaizer.Areas.manage.Controllers
{
    [Area("manage")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(UserManager<AppUser> usermanager,RoleManager<IdentityRole> roleManager)
        {
            _usermanager = usermanager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser user = new AppUser
            {
                
                UserName = "OrSuperAdmin"
            };
            await _usermanager.CreateAsync(user, "OrSuperAdmin");
            return Ok("Admin Created");

        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("User");
        //    await _roleManager.CreateAsync(role3);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role1);
        //    return Ok("Roles Created");
        //}
        public async Task<IActionResult> AddRole()
        {
            AppUser admin =await _usermanager.FindByNameAsync("OrSuperAdmin");
            await _usermanager.AddToRoleAsync(admin, "SuperAdmin");
            return Ok("RoleAdded");

        }
    }
}
