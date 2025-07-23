using System;
using Entities;

namespace Rota.Core.Interfaces
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);

        Task<User> GetUserDetailsAsync(Guid userId);
        Task UpdatePasswordAsync(Guid userId, string newPassword);
        Task<bool> CheckPasswordAsync(Guid userId, string password);
    }
}

