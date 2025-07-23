using System;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class ReportService : IReportService
	{
        private readonly IUnitOfWork _unitOfWork;
        
		public ReportService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<IEnumerable<DailyReservationDto>> GetDailyReservationCountAsync()
        {
            return await _unitOfWork.Reports.GetDailyReservationCountAsync();
        }

        public async Task<int> GetTodayUserCountAsync()
        {
            return await _unitOfWork.Reports.GetTodayUserCountAsync();
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _unitOfWork.Reports.GetTotalUserCountAsync();
        }

        public async Task<IEnumerable<TourDemandDto>> GetTourDemandAsync()
        {
            return await _unitOfWork.Reports.GetTourDemandAsync();
        }

        public async Task<IEnumerable<TourRevenueDto>> GetTourRevenueAsync()
        {
            return await _unitOfWork.Reports.GetTourRevenueAsync();
        }
    }
}

