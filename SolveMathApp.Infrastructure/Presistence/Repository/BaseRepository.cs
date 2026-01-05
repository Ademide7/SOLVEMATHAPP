using Microsoft.EntityFrameworkCore;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly SolveMathDbContext _context; 
		public BaseRepository(SolveMathDbContext context)
		{
			_context = context; 
		}

		public async Task<T?> GetById(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return _context.Set<T>().AsEnumerable();
		}

		public async Task<bool> Add(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return 0 < await _context.SaveChangesAsync();
		}

		public async Task<bool> Update(T entity)
		{
			_context.Set<T>().Update(entity);
			return 0 < await _context.SaveChangesAsync();
		}

		public async Task<bool> Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			return 0 < await _context.SaveChangesAsync();
		}

		public async Task<int> Count()
		{
			return await _context.Set<T>().CountAsync();
		}

	}
}
