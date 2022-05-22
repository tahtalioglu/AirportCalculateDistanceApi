 using AirportCalculateDistanceApi.Services;
namespace AirportCalculateDistanceApi.Infrastructure.Extensions
{
	public static class ApplicationConfiguration
	{
	
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<ICalculateService, CalculateService>();

			return services;
		}
	}
}
