using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Dtos
{
	public record CalculateDistanceDto(double X1, double Y1, double X2, double Y2, Guid UserId);

	public record CalculateDistanceResultDto(double Distance);
}
