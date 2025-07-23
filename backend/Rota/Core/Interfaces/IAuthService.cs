using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface IAuthService
	{
		Task<string> RegisterAsync(RegisterDto dto);
		Task<string> LoginAsync(LoginDto dto);
		Task ForgotPasswordAsync(ForgotPasswordDto dto);
		Task ResetPasswordAsync(ResetPasswordDto dto);
		Task<UserDto> GetByEmailAsync(string email);
    }
}

