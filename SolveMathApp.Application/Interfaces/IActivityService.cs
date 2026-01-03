using SolveMathApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Interfaces
{
	public interface IActivityService
	{
		Task<List<ActivityDto>> GetAllActivities();
	}
}
