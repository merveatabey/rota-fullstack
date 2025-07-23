using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rota.Entities.DTOs;

namespace Rota.Core.Utilities
{
	public class JwtTokenGenerator
	{
		private readonly IConfiguration _config;
		public JwtTokenGenerator(IConfiguration config)
		{
			_config = config;
		}


        public string GeneratorToken(JwtDto jwtDto)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, jwtDto.Id.ToString()), 
        new Claim("id", jwtDto.Id.ToString()), 
        new Claim(ClaimTypes.Email, jwtDto.Email),
        new Claim(ClaimTypes.Role, jwtDto.Role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

