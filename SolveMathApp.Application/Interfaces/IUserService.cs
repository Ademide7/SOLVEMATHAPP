using SolveMathApp.Application.Models;
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
		Task<ResponseModel<ValidateUserDto>> ValidateUser(string email, string password);
        Task<ResponseModel<ValidateUserDto>> RefreshToken(string token, string email); 
        Task<bool> UserExists(string email);
        Task<ResponseModel<PaginationResponse<User>>> GetAllUsers(int page, int pagesize);
		Task<ResponseModel<PaginationResponse<UserActivities>>> GetUserActivities(Guid userId, int page, int pageSize);

	}
}
