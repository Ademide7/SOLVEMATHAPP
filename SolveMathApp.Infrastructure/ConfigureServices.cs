using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolveMathApp.Domain.Abstractions;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Interfaces;
using SolveMathApp.Infrastructure.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace SolveMathApp.Infrastructure
{
	// service registration for infrastructure layer.
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{

			// Add DbContext	 
			if (configuration.GetValue<bool>("UseFakeDatabase"))
			{
				services.AddDbContext<SolveMathDbContext>(o =>
					o.UseSqlite("Data Source=SolveMathAppFakeDb.db"));
			}
			else
			{
				services.AddDbContext<SolveMathDbContext>(o =>
					o.UseSqlServer(
						configuration.GetConnectionString("DefaultConnection")));
			}

			// Add repositories
			services.AddScoped<IUserRepository, UserRepository>(); 
			services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

			return services;
		}
	} 
}
