using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Models
{
    public record ValidateUserDto(bool IsValidUser, string token);
}
