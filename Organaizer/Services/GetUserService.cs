using Microsoft.AspNetCore.Identity;
using Organaizer.Models;

namespace Organaizer.Services
{
    public class GetUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public GetUserService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public async Task<AppUser> GetUser()
        {
            AppUser user = null;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                return user;
            }


            return null;
        }
        public bool IsUserAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
