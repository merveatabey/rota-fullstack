using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Core.Utilities;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class UserManagementService : IUserManagementService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtTokenGenerator _jwt;


        public UserManagementService(IUnitOfWork unitOfWork, IMapper mapper, JwtTokenGenerator jwt)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwt = jwt;

        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _unitOfWork.AdminUsers.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            bool verified = BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash);
            if (!verified)
                throw new Exception("Mevcut şifre yanlış");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<string> CreateUserAsync(UserManagementDto dto)
        {

            var user = new User
            {
                Id = Guid.NewGuid(), 
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            var jwtDto = new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };

            return _jwt.GeneratorToken(jwtDto);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.AdminUsers.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            _unitOfWork.AdminUsers.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserManagementDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.AdminUsers.GetAllAsync();
            return _mapper.Map<IEnumerable<UserManagementDto>>(users);
        }

        public async Task<UserManagementDto> GetMyInfoAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminUsers.GetUserDetailsAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            return _mapper.Map<UserManagementDto>(user);
        }

        public async Task<UserManagementDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.AdminUsers.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            return _mapper.Map<UserManagementDto>(user);
        }

        public async Task UpdateMyInfoAsync(Guid userId, UserManagementDto dto)
        {
            var user = await _unitOfWork.AdminUsers.GetUserDetailsAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            user.FullName = dto.FullName;
            user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            _unitOfWork.AdminUsers.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserAsync(Guid id, UserManagementDto dto)
        {
            var user = await _unitOfWork.AdminUsers.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Role = dto.Role;

            // Parola güncelleme isteğe bağlı
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }
    }
}

