using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class FavoriteTourRepository : GenericRepository<FavoriteTour>, IFavoriteRepository
	{
        private readonly AppDbContext _context;

        public FavoriteTourRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FavoriteTour>> GetFavoritesByUserIdAsync(Guid userId)
        {
            return await _context.FavoriteTours
                .Include(f => f.Tour)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<FavoriteTour> GetByUserIdAndTourIdAsync(Guid userId, int tourId)
        {
            return await _context.FavoriteTours
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TourId == tourId);
        }
    }
}

