using SolveMathApp.Domain.Dtos;
using SolveMathApp.Domain.Entities;
using SolveMathApp.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Interfaces
{
	public interface IUserService
	{
		Task<ResponseModel> CreateUser(UserDto userDto);
		Task<ResponseModel> UpdateUser(User user);
		Task<ResponseModel<bool>> ValidateUserByEmail(string email, string password);
		Task<ResponseModel<List<UserActivities>>> GetUserActivities(Guid userId);

	}
}
