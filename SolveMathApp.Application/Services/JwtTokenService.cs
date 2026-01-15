using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens; 
using SolveMathApp.Application.Interfaces;
using SolveMathApp.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace SolveMathApp.Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
		private readonly IConfiguration _config;

		public JwtTokenService(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateToken(User user)
		{
			var jwt = _config.GetSection("Jwt");

			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			//new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
			//new Claim(ClaimTypes.Role, user.Role)
		};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(jwt["Key"]));

			var creds = new SigningCredentials(
				key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: jwt["Issuer"],
				audience: jwt["Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(
					int.Parse(jwt["ExpireMinutes"])),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
