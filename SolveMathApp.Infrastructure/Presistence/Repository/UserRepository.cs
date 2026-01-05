using Microsoft.EntityFrameworkCore;
using SolveMathApp.Domain.Abstractions;
using SolveMathApp.Domain.Entities;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Repository
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly SolveMathDbContext _context;
		public UserRepository(SolveMathDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

	    //add user
		public async Task<bool> AddUser(User user)
		{
			await _context.Users.AddAsync(user);
			return 0 < await _context.SaveChangesAsync();
		}

		public async Task<List<UserActivities>> GetUserActivitiesByUserId(Guid userId)
		{
			return await _context.UserActivities.Where(ua => ua.UserId == userId).ToListAsync();
		}

		public async Task<bool> UpdateUser(User user)
		{
			_context.Users.Update(user);
			return 0 < await _context.SaveChangesAsync();
		}

		// add user activities
		public async Task<bool> AddUserActivity(UserActivities userActivity)
		{
			await _context.UserActivities.AddAsync(userActivity);
			return 0 < await _context.SaveChangesAsync();
		}

	}
}
