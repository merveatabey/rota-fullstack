using System;
namespace Rota.Entities.DTOs
{
	public class ResetPasswordDto
	{
		public string Token { get; set; } //mailden gelen token
		public string NewPassword { get; set; }  
	}
}

