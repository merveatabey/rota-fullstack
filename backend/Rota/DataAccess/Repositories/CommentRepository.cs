using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.DataAccess.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{

		private readonly AppDbContext _context;

		public CommentRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

        // Belirli bir tura ait tüm yorumları getir (User adıyla birlikte)
        public async Task<List<CommentDto>> GetCommentsByTourIdAsync(int tourId)
        {
            return await _context.Comments
                .Where(c => c.TourId == tourId)
                .Include(c => c.User)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    TourId = c.TourId,
                    CommentText = c.CommentText,
                    Rating = c.Rating,
                    CreatedAt = c.CreatedAt,
                    FullName = c.User.FullName
                }).ToListAsync();
            
        }

        //belirli bir kullanıcıya ait yorumları getir
        public async Task<List<CommentDto>> GetCommentsByUserIdAsync(Guid userId)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId)
                .Include(c => c.Tour)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    TourId = c.TourId,
                    CommentText = c.CommentText,
                    Rating = c.Rating,
                    CreatedAt = c.CreatedAt,
                    FullName = c.User.FullName
                }).ToListAsync();
        }
    }
}

