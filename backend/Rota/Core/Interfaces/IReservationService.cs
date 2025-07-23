using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface IReservationService : IGenericService<ReservationDto>
	{
		Task<List<ReservationDto>> GetByUserIdAsync(Guid userId);
		Task<ReservationDto> CreateWithDetailsAsync(ReservationCreateDto dto);

    }
}

