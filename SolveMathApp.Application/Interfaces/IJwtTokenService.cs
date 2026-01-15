using SolveMathApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Interfaces
{
    public interface IJwtTokenService
    {
		string GenerateToken(User user);

	}
}
