using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Business.Services;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commnetService;

        public CommentController(ICommentService commentService)
        {
            _commnetService = commentService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll()
        {
            var comments = await _commnetService.GetAllAsync();
            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> Create([FromBody] CommentDto dto)
        {
            await _commnetService.CreateAsync(dto);
            return dto;
        }

        [HttpGet("tour/{tourId}")]
        public async Task<ActionResult<CommentDto>> GetByTourId(int tourId)
        {
            var comments = await _commnetService.GetCommentsByTourIdAsync(tourId);
            return Ok(comments);
        }

        // Yorumları kullanıcı ID'ye göre getir
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<CommentDto>> GetByUserId(Guid userId)
        {
            var comments = await _commnetService.GetCommentsByUserIdAsync(userId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetById(int id)
        {
            try
            {
                var comment = await _commnetService.GetByIdAsync(id);
                return Ok(comment);
            }
            catch (Exception)
            {
                return NotFound(new { message = "Comment not found." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDto>> Update(int id, CommentDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "ID mismatch." });

            try
            {
                await _commnetService.UpdateAsync(dto);
                return Ok(new { message = "Comment updated successfully." });
            }
            catch (Exception)
            {
                return NotFound(new { message = "Comment not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentDto>> Delete(int id)
        {
            try
            {
                await _commnetService.DeleteAsync(id);
                return Ok(new { message = "Comment deleted successfully." });
            }
            catch (Exception)
            {
                return NotFound(new { message = "Comment not found." });
            }

        }
    }
}
