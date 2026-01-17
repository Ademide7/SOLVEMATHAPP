using Azure;
using Microsoft.EntityFrameworkCore;
using SolveMathApp.Domain.Abstractions;
using SolveMathApp.Domain.Entities;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Interfaces;
using SolveMathApp.Infrastructure.Presistence.Utilities;
using SolveMathApp.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
			return await _context.Users
	.Include(u => u.Passwords)
	.FirstOrDefaultAsync(u => u.Email == email);
		}

		//add user
		public async Task<bool> AddUser(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.Passwords.AddAsync(user?.Passwords?.First());
			return 0 < await _context.SaveChangesAsync();
		}

		public async Task<PaginationResponse<UserActivities>> GetUserActivitiesByUserId(Guid userId, int page ,int pageSize)
		{
			return await PaginationHelper.PaginateAsync<UserActivities>(_context.UserActivities.Where(ua => ua.UserId == userId).OrderByDescending(m=>m.DateCreated).AsNoTracking(),page,pageSize);
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

		//get all users .
		public async Task<PaginationResponse<User>> GetAllUsers(int page, int size)
		{

			IQueryable<User> query = _context.Users.Where(x => x.Id != Guid.Empty);
			return await PaginationHelper.PaginateAsync(query, page,size);
		}

		// check if user exists by email.
		public async Task<bool> UserExists(string email)
		{
			return await _context.Users.AnyAsync(u => u.Email == email);
		}
	}
}
