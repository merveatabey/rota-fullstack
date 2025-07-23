using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface IUserManagementService
	{
        Task<IEnumerable<UserManagementDto>> GetAllUsersAsync();
        Task<UserManagementDto> GetUserByIdAsync(Guid id);
        Task<string> CreateUserAsync(UserManagementDto dto);
        Task UpdateUserAsync(Guid id, UserManagementDto dto);
        Task DeleteUserAsync(Guid id);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        Task<UserManagementDto> GetMyInfoAsync(Guid userId);
        Task UpdateMyInfoAsync(Guid userId, UserManagementDto dto);

    }
}

