using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks; 

namespace SolveMathApp.SharedKernel.Models
{
	// pagaination response record
	public record PaginationResponse<T>(IEnumerable<T> Items, int TotalCount, int PageSize, int CurrentPage)
		: ResponseModel<IEnumerable<T>>(Items, "Paginated data retrieved successfully", true);


	
}
