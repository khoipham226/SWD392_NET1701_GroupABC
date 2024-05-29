using BusinessLayer.Services;
using DataLayer.DBContext;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SWDProject_BE.AppStarts
{
	public static class DependencyInjectionContainers
	{
		public static void InstallService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddRouting(options =>
			{
				options.LowercaseUrls = true; ;
				options.LowercaseQueryStrings = true;
			});
			//Add DBContext
			services.AddDbContext<MyDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("local"));
			});

			//MapData
			services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //MapService
            services.AddScoped<IProductService, ProductService>();
				
			//services.AddScoped<IJwtService, JwtService>();
			
		}
	}
}
