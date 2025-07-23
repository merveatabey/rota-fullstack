using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet("total-user-count")]
        public async Task<IActionResult> GetTotalUserCount()
        {
            try
            {
                var count = await _reportService.GetTotalUserCountAsync();
                return Ok(count);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpGet("today-user-count")]
        public async Task<IActionResult> GetTodayUserCount()
        {
            var count = await _reportService.GetTodayUserCountAsync();
            return Ok(count);
        }

        [HttpGet("tour-demands")]
        public async Task<IActionResult> GetTourDemands()
        {
            var tourDemands = await _reportService.GetTourDemandAsync();
            return Ok(tourDemands);
        }

        [HttpGet("tour-revenues")]
        public async Task<IActionResult> GetTourRevenues()
        {
            var revenues = await _reportService.GetTourRevenueAsync();
            return Ok(revenues);
        }

        [HttpGet("daily-reservations")]
        public async Task<IActionResult> GetDailyReservations()
        {
            try
            {
                var dailyReservations = await _reportService.GetDailyReservationCountAsync();
                return Ok(dailyReservations);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }
    }
}

