using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Interfaces
{
	public interface IUserRepository
	{
		Task<Domain.Entities.User?> GetUserByEmail(string email);
		Task<bool> AddUser(Domain.Entities.User user);
		Task<List<Domain.Entities.UserActivities>> GetUserActivitiesByUserId(Guid userId);
		Task<bool> UpdateUser(Domain.Entities.User user);
		Task<bool> AddUserActivity(Domain.Entities.UserActivities userActivity);
	}
}
