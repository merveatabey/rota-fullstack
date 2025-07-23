using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rota.Business.Services;
using Rota.Core.Interfaces;
using Rota.DataAccess.Repositories;
using Rota.Entities.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpGet]
        public async Task<IEnumerable<ReservationDto>> GetAll()
        {
            return await _reservationService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with id {id} not found.");
            }

            return reservation;
        }


        [HttpPost("create")]
        public async Task<ActionResult<ReservationDto>> Create([FromBody] ReservationDto dto)
        {
            await _reservationService.CreateAsync(dto);
            return dto;
        }


        [Authorize]
        [HttpPost("create-with-details")]
        public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] ReservationCreateDto dto)
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (userIdClaim == null)
            {
                return Unauthorized("Kullanıcı bulunamadı.");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            // Kullanıcının tüm rezervasyonlarını al
            var userReservations = await _reservationService.GetByUserIdAsync(userId);

            // Aynı tur için rezervasyon var mı kontrol et
            var exists = userReservations.Any(r => r.TourId == dto.TourId);

            if (exists)
            {
                return BadRequest("Bu tur için zaten bir rezervasyonunuz bulunmaktadır.");
            }

            dto.UserId = userId; // UserId'yi dto'ya set et

            // Rezervasyon oluştur
            var reservation = await _reservationService.CreateWithDetailsAsync(dto);

            return Ok(reservation);
        }


        // GET: api/reservation/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(Guid userId)
        {
            try
            {
                var reservations = await _reservationService.GetByUserIdAsync(userId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> Update(int id, [FromBody]ReservationDto dto)
        {
            dto.Id = id;
            var existing = await _reservationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Reservation with id {id} not found");
            await _reservationService.UpdateAsync(dto);
            return dto;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationDto>> Delete(int id)
        {
            var existing = await _reservationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Reservation with id {id} not found");
            await _reservationService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpGet("my-reservations")]
        public async Task<IActionResult> GetMyReservations()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var reservations = await _reservationService.GetByUserIdAsync(Guid.Parse(userId));
            return Ok(reservations);
        }


        [HttpPost("confirm/{reservationId}")]
        public async Task<IActionResult> ConfirmPayment(int reservationId)
        {
            // Rezervasyonu al
            var reservation = await _reservationService.GetByIdAsync(reservationId);
            if (reservation == null)
                return NotFound($"Reservation with id {reservationId} not found.");

            // Status güncelle
            reservation.Status = "Satıldı";  // veya "Satın Alındı"

            await _reservationService.UpdateAsync(reservation);

            return Ok(new { message = "Ödeme onaylandı ve rezervasyon güncellendi." });
        }


        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> ApproveReservation(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();
            reservation.Status = "Rezerve";
            await _reservationService.UpdateAsync(reservation);
            return NoContent();
        }

    }
}

