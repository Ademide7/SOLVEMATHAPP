using SolveMathApp.ServerApp.Data.Services.Models;
using SolveMathApp.ServerApp.Data.Services.SolveMathService;

namespace SolveMathApp.ServerApp.Data
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
		{ 
			// Add repositories
			services.AddScoped<ISolveMathApi, SolveMathApi>();
			// add ioptions pattern for SolveMathApiSettings.

			// Bind Jwt section and enable IOptionsMonitor
			services.Configure<SolveMathApiSettings>(
			configuration.GetSection("SolveMathApiSettings"));
		
			return services;
		}
	}
}
