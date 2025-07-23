using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface IReportRepository
	{
		Task<int> GetTotalUserCountAsync();
		Task<int> GetTodayUserCountAsync();
		Task<IEnumerable<TourDemandDto>> GetTourDemandAsync();
		Task<IEnumerable<TourRevenueDto>> GetTourRevenueAsync();
		Task<IEnumerable<DailyReservationDto>> GetDailyReservationCountAsync();
	}
}

