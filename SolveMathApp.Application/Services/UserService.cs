using SolveMathApp.Application.Interfaces;
using SolveMathApp.Application.Validations;
using SolveMathApp.Domain.Dtos;
using SolveMathApp.Domain.Entities;
using SolveMathApp.SharedKernel;
using SolveMathApp.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Services
{
	public class UserService() : IUserService
	{
		public async Task<ResponseModel> CreateUser(UserDto userDto)
		{
			var validation = UserValidations.ValidateCreateUser(userDto);
			if(validation.status is false) return new ResponseModel(validation.errorMessage,validation.status);

			return new ResponseModel("User created successfully!",true);
		}

		public async Task<ResponseModel> UpdateUser(User user)
		{

			return new ResponseModel("User updated successfully!", true);
		}

		public async Task<ResponseModel<bool>> ValidateUserByEmail(string email, string password)
		{
			var validation = Utilities.ValidateEmail(email);
			if (validation) return new ResponseModel<bool>(false,"Invalid Credentials!",false);

			return new ResponseModel<bool>(true, "Valid User!", true);
		}

		public async Task<ResponseModel<List<UserActivities>>> GetUserActivities(Guid userId)
		{ 

			return new ResponseModel<List<UserActivities>>(new List<UserActivities> { }, "Done Successfully!", true);
		}

	}
}
