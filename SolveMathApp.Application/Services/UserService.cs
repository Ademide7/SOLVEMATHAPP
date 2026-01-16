using SolveMathApp.Application.Interfaces;
using SolveMathApp.Application.Models;
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
	public class UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService) : IUserService
	{
		public async Task<ResponseModel> CreateUser(UserDto userDto)
		{
			var validation = UserValidations.ValidateCreateUser(userDto);
			if(validation.status is false) return new ResponseModel(validation.errorMessage,validation.status);

			// CHECK IF USER WITH THE EMAIL ALREADY EXISTS 
			var user = new User().CreateUser(userDto);

			bool existingUser = await userRepository.UserExists(user.Email);
			if(existingUser) return new ResponseModel("User with the provided email already exists!", false);
			await userRepository.AddUser(user);

			return new ResponseModel("User created successfully!",true);
		}

		public async Task<ResponseModel> UpdateUser(User user)
		{
			await userRepository.UpdateUser(user);
			return new ResponseModel("User updated successfully!", true);
		}

		public async Task<ResponseModel<ValidateUserDto>> ValidateUser(string email, string password)
		{
			var validation = Utilities.ValidateEmail(email);
			if (!validation) return new ResponseModel<ValidateUserDto>(new ValidateUserDto(false, string.Empty), "Invalid Credentials!",false);

			var user = await userRepository.GetUserByEmail(email);
			if (user == null) return new ResponseModel<ValidateUserDto>(new ValidateUserDto(false, string.Empty), "Invalid Credentials!", false);
			var encryptedPassword = Utilities.EncryptPassword(password);
			if (user?.Passwords?.OrderByDescending( m => m.DateCreated)?.FirstOrDefault()?.Value != encryptedPassword) return new ResponseModel<ValidateUserDto>(new ValidateUserDto(false, string.Empty), "Invalid Credentials!", false);
			
			Console.WriteLine("User validated successfully!");
			string token = jwtTokenService.GenerateToken(user);
			Console.WriteLine($"Token Generated for {user.Email}");

			return new ResponseModel<ValidateUserDto>(new ValidateUserDto(true , token),"Valid User!", true);
		}

		//refresh token.
		public async Task<ResponseModel<ValidateUserDto>> RefreshToken(string token, string email)
		{
			 var user = await userRepository.GetUserByEmail(email);
			if (user == null) return new ResponseModel<ValidateUserDto>(new ValidateUserDto(false, string.Empty), "Invalid Token!", false);
			var newToken = jwtTokenService.GenerateToken(user);
			return new ResponseModel<ValidateUserDto>(new ValidateUserDto(true, newToken), "Token Refreshed Successfully!", true);
		}

		public async Task<ResponseModel<PaginationResponse<UserActivities>>> GetUserActivities(Guid userId,int page,int pageSize)
		{ 
			var userActivities = await userRepository.GetUserActivitiesByUserId(userId,page,pageSize);
			if (userActivities.PageSize > 0)
			{
				return new ResponseModel<PaginationResponse<UserActivities>>(userActivities, "Done Successfully!", true);
			}
			return new ResponseModel<PaginationResponse<UserActivities>>(new PaginationResponse<UserActivities>(null,0,0,0), "Done Successfully!", true);
		}

	    // GET ALL USERS
		public async Task<ResponseModel<PaginationResponse<User>>> GetAllUsers(int page, int pagesize)
		{
			var users = await userRepository.GetAllUsers(page, pagesize);
			return new ResponseModel<PaginationResponse<User>>(users, "Users retrieved successfully!", true);
		}

		public async Task<bool> UserExists(string email)
		{
			return await userRepository.UserExists(email);
		}

	}
}
