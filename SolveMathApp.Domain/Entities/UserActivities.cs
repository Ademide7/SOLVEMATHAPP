using SolveMathApp.Domain.Dtos;
using SolveMathApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Entities
{
	public class UserActivities
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public ActivityEnum ActivityId { get; set; } = ActivityEnum.None;
		public string Description { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; }
		
		public UserActivities() => Id = Guid.NewGuid();

		public static UserActivities CreateUserActivities (UserActivitiesDto userActivitiesDto)
		{
			UserActivities userActivities = new UserActivities();
			userActivities.ActivityId = userActivitiesDto.ActivityId;
			userActivities.UserId = userActivitiesDto.UserId;
			userActivities.Description = userActivitiesDto.description;
			userActivities.DateCreated = DateTime.UtcNow.AddHours(1);	
			return userActivities;
		} 

	}
}
