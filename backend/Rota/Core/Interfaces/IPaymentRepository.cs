using System;
using Entities;

namespace Rota.Core.Interfaces
{
	public interface IPaymentRepository : IGenericRepository<Payment>
	{
        Task<Payment> GetPaymentByReservationIdAsync(int reservationId);

    }
}

