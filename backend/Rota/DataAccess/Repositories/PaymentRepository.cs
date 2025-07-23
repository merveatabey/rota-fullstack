using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByReservationIdAsync(int reservationId)
        {
            return await _context.Payments
              .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
        }
    }
}

