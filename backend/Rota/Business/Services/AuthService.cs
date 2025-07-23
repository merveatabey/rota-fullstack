using System;
using Entities;
using Rota.Core.Interfaces;
using Rota.Core.Utilities;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly JwtTokenGenerator _jwt;
        private readonly IEmailService _emailService;

        public AuthService(IUnitOfWork unitOfWork, JwtTokenGenerator jwt, IEmailService emailService)
		{
			_unitOfWork = unitOfWork;
			_jwt = jwt;
            _emailService = emailService;

		}

        public async Task<bool> CheckUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Email == email);
            return user != null;
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Email == dto.Email);

            if(user == null)
            {
                throw new Exception("User not found");
            }


            var token = new Random().Next(100000, 999999).ToString(); //6 haneli kod olarak gönder token'ı
            user.ResetToken = token;
            user.ResetTokenExpiration = DateTime.UtcNow.AddMinutes(15);

            await _unitOfWork.SaveAsync();


            //mail gönder
            var resetLink = $"http://localhost:3000/reset-password?token={token}";
            await _emailService.SendPasswordResetMail(user.Email, resetLink);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _unitOfWork.AdminUsers.GetByEmailAsync(email);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                FullName = user.FullName
            };

        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = (await _unitOfWork.Users.FindAsync(u => u.Email == dto.Email)).FirstOrDefault();

            if(user == null)
            {
                throw new Exception("kullanıcı bulunamadı");
            }
        

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("şifre hatalı");
            }

            var userDto = new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };

            return _jwt.GeneratorToken(userDto);
        }



        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            //email'in var olup olmadığını kontrol et
            var existingUser = (await _unitOfWork.Users.FindAsync(u => u.Email == dto.Email)).FirstOrDefault();
            if(existingUser != null)
            {
                throw new Exception("Bu e-posta zaten kayıtlı");
            }

            // Sadece normal kullanıcı kaydı yapılabilir, rol hep "User"
            var role = "User";


            //yeni kullanıcı oluştur
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = role,
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


        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.ResetToken == dto.Token);

            if (user == null)
                throw new Exception("Invalid or expired token");

            if (user.ResetTokenExpiration < DateTime.UtcNow)
                throw new Exception("Token expired");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiration = null;

            await _unitOfWork.SaveAsync();
        }

    }
}

