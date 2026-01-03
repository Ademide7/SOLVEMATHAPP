using SolveMathApp.Application.Interfaces;
using SolveMathApp.Domain.Dtos;
using SolveMathApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Services
{
	public class ActivityService() : IActivityService
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

		public async Task




	}
}
