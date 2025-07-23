using System;
namespace Rota.Core.Utilities
{
	public class EmailSettings
	{
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SenderMail { get; set; }
		public string SenderPassword { get; set; }
	}
}

