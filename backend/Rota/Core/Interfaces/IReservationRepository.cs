using System;
using Entities;

namespace Rota.Core.Interfaces
{
	public interface IReservationRepository : IGenericRepository<Reservation>
	{
		Task<List<Reservation>> GetReservationsByUserAsync(Guid userId);
		Task<List<Reservation>> GetReservationsWithTourAsync();
        Task<Reservation> GetReservationWithDetailsAsync(int id);



    }
}

