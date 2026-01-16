using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Dtos
{
	public  record CalculateFactorialDto(int Number, Guid UserId);

	public record CalculateFactorialRequest(int Number);

	public  record CalculateFactorialResultDto(long Result);
}
