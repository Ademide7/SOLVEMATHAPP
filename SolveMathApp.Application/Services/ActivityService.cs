using Newtonsoft.Json;
using SolveMathApp.Application.Interfaces;
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
	public class ActivityService(IUserRepository userRepository) : IActivityService
	{
		public async Task<List<ActivityDto>> GetAllActivities()
		{
			List<ActivityDto> activities = new List<ActivityDto>();
			foreach (var activity in Enum.GetValues(typeof(ActivityEnum)))
			{ 
				activities.Add(new ActivityDto((int)activity, activity.ToString()));
			}
			return await Task.FromResult(activities);
		}

		public async Task<ResponseModel<CalculateFactorialResultDto>> CalculateFactorial(CalculateFactorialDto dto)
		{
			if (dto.Number < 0) 			
			return new ResponseModel<CalculateFactorialResultDto>
					(null,"Value must be greater than zero",false);
	
			var result = Utilities.CalculateFactorial(dto.Number);

			_ = AddUserActivity(dto.UserId, ActivityEnum.CalculateFactorial, JsonConvert.SerializeObject(dto));

			return new ResponseModel<CalculateFactorialResultDto>
			(new CalculateFactorialResultDto(result), "Value must be greater than zero", false);
		}

		public async Task<ResponseModel<CalculateAgeResultDto>> CalculateAge(CalculateAgeDto dto)
		{
			if (dto.BirthYear == DateTime.MinValue)
				return new ResponseModel<CalculateAgeResultDto>
						(null, "Invalid Input", false);

			var result = Utilities.CalculateAgeFromCurrentDate(dto.BirthYear);

			_ = AddUserActivity(dto.UserId, ActivityEnum.CalculateAgeFromCurrentDate, JsonConvert.SerializeObject(dto));


			return new ResponseModel<CalculateAgeResultDto>
			(new CalculateAgeResultDto(result), "Done successfully", true);
		}

		public async Task<ResponseModel<CalculateDistanceResultDto>> CalculateDistance(CalculateDistanceDto dto)
		{
			var result = Utilities.CalculateDistance(dto.X1,dto.Y1,dto.X2,dto.Y2);

			_ = AddUserActivity(dto.UserId, ActivityEnum.CalculateDistance, JsonConvert.SerializeObject(dto));

			return new ResponseModel<CalculateDistanceResultDto>
			(new CalculateDistanceResultDto(result), "Done successfully", true);
		}

		//add user activity.
		public async Task<ResponseModel<bool>> AddUserActivity(Guid userId, ActivityEnum activityType, string description)
		{
			// Implementation to log user activity.
			var userActivity = UserActivities.CreateUserActivities(new UserActivitiesDto(userId, activityType, description));
			var status = await userRepository.AddUserActivity(userActivity);
			if (!status)
			{
			    return new ResponseModel<bool>(false, "Failed to log user activity", false);
			}

			return new ResponseModel<bool>(true, "User activity logged successfully", true); 
		}

		// get all activitieslogged by userid
		public async Task<ResponseModel<PaginationResponse<UserActivities>>> GetUserActivities(Guid userId, int page, int pageSize)
		{
			var userActivities = await userRepository.GetUserActivitiesByUserId(userId,page,pageSize);
			if (userActivities.PageSize >= 0)
			{
				return new ResponseModel<PaginationResponse<UserActivities>>(userActivities, "Done Successfully!", true);
			}
			return new ResponseModel<PaginationResponse<UserActivities>>(null, "No activities found for the user.", false);
		}


	}
}
