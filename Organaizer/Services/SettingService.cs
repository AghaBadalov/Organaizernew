using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using Organaizer.Models;

namespace Organaizer.Services
{
    public class SettingService
    {
        private readonly AppDBContext _context;

        public SettingService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Setting>> GetSettings()
        {
            return await _context.Settings.ToListAsync();
        }
    }
}
