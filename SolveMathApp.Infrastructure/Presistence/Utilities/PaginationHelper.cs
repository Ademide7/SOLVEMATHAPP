using Microsoft.EntityFrameworkCore;
using SolveMathApp.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Utilities
{
	public static class PaginationHelper
	{
		public static async Task<PaginationResponse<T>> PaginateAsync<T>(
			IQueryable<T> query,
			int page,
			int pageSize,
			CancellationToken cancellationToken = default)
		{
			if (page <= 0) page = 1;
			if (pageSize <= 0) pageSize = 10;

			var totalCount = await query.CountAsync(cancellationToken);

			var items = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);

			return new PaginationResponse<T>(
				items,
				totalCount,
				pageSize,
				page
			);
		}
	}
}
