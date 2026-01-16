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
	public interface IActivityService
	{
		Task<ResponseModel<CalculateFactorialResultDto>> CalculateFactorial(CalculateFactorialDto dto);
		Task<ResponseModel<CalculateAgeResultDto>> CalculateAge(CalculateAgeDto dto);
		Task<List<ActivityDto>> GetAllActivities();
		Task<ResponseModel<CalculateDistanceResultDto>> CalculateDistance(CalculateDistanceDto dto);
        Task<ResponseModel<List<UserActivities>>> GetUserActivities(Guid userId);
    }
}
