using SolveMathApp.Application.Interfaces;
using SolveMathApp.Application.Validations;
using SolveMathApp.Domain.Abstractions;
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
	public class UserService(IUserRepository userRepository) : IUserService
	{
		public async Task<ResponseModel> CreateUser(UserDto userDto)
		{
			var validation = UserValidations.ValidateCreateUser(userDto);
			if(validation.status is false) return new ResponseModel(validation.errorMessage,validation.status);
			var user = new User().CreateUser(userDto);

			var existingUser = await userRepository.GetUserByEmail(user.Email);
			if(existingUser != null) return new ResponseModel("User with the provided email already exists!", false);
			await userRepository.AddUser(user);

			return new ResponseModel("User created successfully!",true);
		}

		public async Task<ResponseModel> UpdateUser(User user)
		{
			await userRepository.UpdateUser(user);
			return new ResponseModel("User updated successfully!", true);
		}

		public async Task<ResponseModel<bool>> ValidateUserByEmail(string email, string password)
		{
			var validation = Utilities.ValidateEmail(email);
			if (validation) return new ResponseModel<bool>(false,"Invalid Credentials!",false);

			var user = await userRepository.GetUserByEmail(email);
			if (user == null) return new ResponseModel<bool>(false, "Invalid Credentials!", false);
			var encryptedPassword = Utilities.EncryptPassword(password);
			if (user.Password.Value != encryptedPassword) return new ResponseModel<bool>(false, "Invalid Credentials!", false);
            
			return new ResponseModel<bool>(true, "Valid User!", true);
		}

		public async Task<ResponseModel<List<UserActivities>>> GetUserActivities(Guid userId)
		{ 
			var userActivities = await userRepository.GetUserActivitiesByUserId(userId);
			if (userActivities.Count > 0)
			{
				return new ResponseModel<List<UserActivities>>(userActivities, "Done Successfully!", true);
			}
			return new ResponseModel<List<UserActivities>>(new List<UserActivities> { }, "Done Successfully!", true);
		}

	}
}
