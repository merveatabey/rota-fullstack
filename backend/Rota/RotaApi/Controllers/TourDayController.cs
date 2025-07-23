using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourDayController : ControllerBase
    {

        private readonly ITourDayService _tourDayService;

        public TourDayController(ITourDayService tourDayService)
        {
            _tourDayService = tourDayService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourDayDto>>>GetAll()
        {
            var tourDays = await _tourDayService.GetAllAsync();
            return Ok(tourDays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourDayDto>> GetById(int id)
        {
            var tourDay = await _tourDayService.GetByIdAsync(id);
            if (tourDay == null)
                return NotFound($"TourDay with id {id} not found.");
            return Ok(tourDay);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TourDayDto dto)
        {
            await _tourDayService.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]TourDayDto dto)
        {
            dto.Id = id;

            var existing = await _tourDayService.GetByIdAsync(id);
            if(existing == null)
            {
                return NotFound($"TourDay with id {id} not found");
            }

            await _tourDayService.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _tourDayService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"TourDay with id {id} not found.");

            await _tourDayService.DeleteAsync(id);
            return NoContent();
        }
    }
}

