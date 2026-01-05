using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		Task<T?> GetById(int id);
		Task<IEnumerable<T>> GetAll();
		Task<bool> Add(T entity);
		Task<bool> Update(T entity);
		Task<bool> Delete(T entity);
		Task<int> Count();
	}
}
