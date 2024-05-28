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
			//services.AddDbContext<StarasContext>(options =>
			//{
			//	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			//});

			//services.AddScoped<IUnitOfWork, UnitOfWork>();
			
			//services.AddScoped<IJwtService, JwtService>();
			
		}
	}
}
