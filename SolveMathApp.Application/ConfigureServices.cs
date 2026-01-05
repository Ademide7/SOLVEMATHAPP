using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolveMathApp.Application.Interfaces;
using SolveMathApp.Application.Services;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Interfaces;
using SolveMathApp.Infrastructure.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Add repositories
			services.AddScoped<IActivityService, ActivityService>(); 
			services.AddScoped<IUserService, UserService>();

			return services;
		}
	}
}
