namespace Organaizer.Models
{
    using AutoMapper;

    public class Startup
    {
        

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));
            
        }
    }

}
