using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
	{
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservationsByUserAsync(Guid userId)
        {
            return await _context.Reservations
                .Include(r => r.Tour)
                .Where(r => r.UserId == userId)
                .ToListAsync();
                
        }

        public async Task<List<Reservation>> GetReservationsWithTourAsync()
        {
            return await _context.Reservations
                .Include(r => r.Tour)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationWithDetailsAsync(int id)
        {
            return await _context.Reservations
               .Include(r => r.Tour)
               .Include(r => r.User)
               .Include(r => r.Payment)
               .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

