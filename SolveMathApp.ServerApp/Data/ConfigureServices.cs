using Microsoft.AspNetCore.Components.Authorization;
using SolveMathApp.ServerApp.Data.Services.AuthenticationService;
using SolveMathApp.ServerApp.Data.Services.Models;
using SolveMathApp.ServerApp.Data.Services.SolveMathService;
using SolveMathApp.ServerApp.Providers;

namespace SolveMathApp.ServerApp.Data
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
		{ 
			// Add repositories
			services.AddScoped<ISolveMathApi, SolveMathApi>();
			services.AddHttpClient();
			services.AddScoped<ApiAuthenticationStateProvider>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

			// add ioptions pattern for SolveMathApiSettings.

			// Bind Jwt section and enable IOptionsMonitor
			services.Configure<SolveMathApiSettings>(
			configuration.GetSection("SolveMathApiSettings"));
			 
			return services;
		}
	}
}
