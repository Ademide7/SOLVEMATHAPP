using SolveMathApp.Domain.Entities;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Seeding
{
	public static class FakeDatabaseSeeder
	{
		public static void Seed(SolveMathDbContext context)
		{
			if (context.Users.Any())
				return;

			// Seed fake users
			context.Users.Add(new User { Name = "Ibrahim", Email = "Kenny",  DateCreated = DateTime.UtcNow.AddHours(1) , DateModified = DateTime.UtcNow.AddHours(1), Id = new Guid()});
			context.Users.Add(new User { Name = "John", Email = "Doe", DateCreated = DateTime.UtcNow.AddHours(1), DateModified = DateTime.UtcNow.AddHours(1), Id = new Guid() });
			context.Users.Add(new User { Name = "Alice", Email = "Smith", DateCreated = DateTime.UtcNow.AddHours(1), DateModified = DateTime.UtcNow.AddHours(1), Id = new Guid() });
			context.Users.Add(new User { Name = "Bob", Email = "Johnson", DateCreated = DateTime.UtcNow.AddHours(1), DateModified = DateTime.UtcNow.AddHours(1), Id = new Guid() });
			context.Users.Add(new User { Name = "Charlie", Email = "Brown", DateCreated = DateTime.UtcNow.AddHours(1), DateModified = DateTime.UtcNow.AddHours(1), Id = new Guid() });

			//context.UserActivities.Add(
			//	new UserActivities
			//	{
			//		UserId = context.Users.First().Id,
			//		ActivityId = ActivityEnum.CalculateAgeFromCurrentDate,
			//		Description = "Calculated age for birth year 1990",
			//		DateCreated = DateTime.UtcNow.AddHours(1)
			//	} 
			//);

			//context.UserActivities.Add( 
			//	new UserActivities
			//	{
			//		UserId = context.Users.First().Id,
			//		ActivityId = ActivityEnum.CalculateFactorial,
			//		Description = "Calculated factorial for number 5",
			//		DateCreated = DateTime.UtcNow.AddHours(1)
			//	}
			//);

			// add passwords to users
			foreach (var user in context.Users)
			{
				user.Passwords.Add(new Password
				{
					Value = Utilities.EncryptPassword("Password123!"),
					Id = Guid.NewGuid(),
					DateCreated = DateTime.UtcNow.AddHours(1),
					UserId = user.Id
				});
			} 
			context.SaveChanges();
		}
	}

}
