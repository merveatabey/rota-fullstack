using System;
namespace Rota.Core.Interfaces
{
	public interface IEmailService
	{
		Task SendPasswordResetMail(string toMail, string resetLink);
	}
}

