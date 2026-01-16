using SolveMathApp.Domain.Entities;
using SolveMathApp.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Abstractions
{
	public interface IUserRepository
	{
		Task<Entities.User?> GetUserByEmail(string email);
		Task<bool> AddUser(Entities.User user); 
		Task<PaginationResponse<UserActivities>> GetUserActivitiesByUserId(Guid userId, int page, int pageSize);
		Task<bool> UpdateUser(Entities.User user);

		Task<bool> AddUserActivity(Entities.UserActivities userActivity); 
        Task<bool> UserExists(string email);

		Task<PaginationResponse<User>> GetAllUsers(int page, int size);

	}
}
