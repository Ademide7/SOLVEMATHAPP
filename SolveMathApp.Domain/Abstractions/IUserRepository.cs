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
		Task<List<Entities.UserActivities>> GetUserActivitiesByUserId(Guid userId);
		Task<bool> UpdateUser(Entities.User user);
		Task<bool> AddUserActivity(Entities.UserActivities userActivity);
	}
}
