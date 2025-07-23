using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
        private AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckPasswordAsync(Guid userId, string password)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            // Eğer hash kullanıyorsan burada hash karşılaştırması yap
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserDetailsAsync(Guid userId)
        {
            return await _context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}

