using Microsoft.EntityFrameworkCore;
using SolveMathApp.Domain.Entities;

namespace SolveMathApp.Infrastructure.Presistence.DbContexts
{
	public class SolveMathDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Password> Passwords { get; set; }
		public DbSet<UserActivities> UserActivities { get; set; }

		public SolveMathDbContext(DbContextOptions<SolveMathDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		} 

	}
}
