﻿using System;
namespace Rota.Entities.DTOs
{
	public class UserManagementDto
	{
        public Guid Id { get; set; }
        public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

	}
}

