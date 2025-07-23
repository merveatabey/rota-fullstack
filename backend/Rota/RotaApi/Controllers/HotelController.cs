using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET api/hotel
        [HttpGet]
        public async Task<IEnumerable<HotelDto>> GetAll()
        {
            return await _hotelService.GetAllAsync();
        }

        // GET api/hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetById(int id)
        {
            var hotel = await _hotelService.GetByIdAsync(id);
            if (hotel == null)
                return NotFound($"Hotel with id {id} not found.");

            return hotel;
        }

        // POST api/hotel
        [HttpPost]
        public async Task<ActionResult<HotelDto>> Create([FromBody] HotelDto dto)
        {
            await _hotelService.CreateAsync(dto);
            // Eğer CreateAsync sonrası dto güncelleniyorsa dönebilirsin:
            return dto;
        }

        // PUT api/hotel/5
        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDto>> Update(int id, [FromBody] HotelDto dto)
        {
            dto.Id = id;

            var existing = await _hotelService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Hotel with id {id} not found.");

            await _hotelService.UpdateAsync(dto);
            return dto;
        }

        // DELETE api/hotel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _hotelService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Hotel with id {id} not found.");

            await _hotelService.DeleteAsync(id);
            return NoContent();
        }
    }
}
