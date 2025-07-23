using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface IPaymentService : IGenericService<PaymentDto>
	{
        Task<PaymentDto> GetByReservationIdAsync(int reservationId);

    }
}

