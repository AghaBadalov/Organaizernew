using AutoMapper;
using Organaizer.ViewModels;

namespace Organaizer.Models
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterVM, AppUser>();
        }
    }
}
