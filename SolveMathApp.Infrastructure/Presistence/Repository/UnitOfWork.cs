using SolveMathApp.Domain.Abstractions;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Infrastructure.Presistence.Repository
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly SolveMathDbContext _context;
        public IUserRepository Users { get; }
		public UnitOfWork(SolveMathDbContext context, IUserRepository userRepository)
        {
            _context = context;
            this.Users = userRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
		}
	}
}
