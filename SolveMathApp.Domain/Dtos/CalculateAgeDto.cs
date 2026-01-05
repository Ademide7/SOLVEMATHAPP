using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Dtos
{
	public record CalculateAgeDto(DateTime BirthYear, Guid UserId);

	public record CalculateAgeResultDto(int Age);
}
