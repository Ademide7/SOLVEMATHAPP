using SolveMathApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Dtos
{
	public record UserActivitiesDto(
	Guid UserId,
	ActivityEnum ActivityId, string description); 
}
