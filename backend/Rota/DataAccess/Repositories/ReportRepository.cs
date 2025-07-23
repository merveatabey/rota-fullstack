using System;
using Dapper;
using DataAccess;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.DataAccess.Repositories
{
    public class ReportRepository : IReportRepository
	{
        private readonly DapperContext _context;

        public ReportRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyReservationDto>> GetDailyReservationCountAsync()
        {
            return await _context.QueryAsync<DailyReservationDto>("GetDailyReservationCount");
        }

        public async Task<int> GetTodayUserCountAsync()
        {
            var param = new DynamicParameters();
            param.Add("@Today", DateTime.Today);

            return await _context.QuerySingleAsync<int>("GetTodayUser", param);
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            var result = await _context.QuerySingleAsync<int>("GetTotalUserCount");
            return result;
        }

        public async Task<IEnumerable<TourDemandDto>> GetTourDemandAsync()
        {
            return await _context.QueryAsync<TourDemandDto>("GetTourDemands");
        }

        public async Task<IEnumerable<TourRevenueDto>> GetTourRevenueAsync()
        {
            return await _context.QueryAsync<TourRevenueDto>("GetTourRevenues");
        }
    }
}

