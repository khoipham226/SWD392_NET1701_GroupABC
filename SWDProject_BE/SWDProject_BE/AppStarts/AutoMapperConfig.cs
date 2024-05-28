using AutoMapper;

namespace SWDProject_BE.AppStarts
{
	public static class AutoMapperConfig
	{
		public static void ConfigureAutoMapper(this IServiceCollection services)
		{
			MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
			{
				//mc.ConfigStoreModule();
			});
			IMapper mapper = mapperConfiguration.CreateMapper();
			services.AddSingleton(mapper);
		}
	}
}
