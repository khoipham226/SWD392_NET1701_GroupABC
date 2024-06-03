using BusinessLayer.Services;
using BusinessLayer.Services.Implements;
using DataLayer.Model;
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
			services.AddDbContext<SWD392_DBContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("local"));
			});

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			
			//services.AddScoped<IJwtService, JwtService>();
			services.AddScoped<IAuthServices, AuthServices>();
			services.AddScoped<IPostService, PostService>();
			services.AddScoped<IUsersService, UsersServices>();
            services.AddTransient<IPaymentService, PaymentService>();

        }
	}
}
