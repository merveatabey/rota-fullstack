using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;
using System.Data;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoriteTourController : ControllerBase
{
    private readonly IFavoriteTourService _favoriteService;

    public FavoriteTourController(IFavoriteTourService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var userId = GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var favorites = await _favoriteService.GetFavoritesByUserIdAsync(userId);
        return Ok(favorites);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var favorite = await _favoriteService.GetByIdAsync(id);
        if (favorite == null) return NotFound();
        return Ok(favorite);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteTourAddDto dto)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var result = await _favoriteService.AddFavoriteAsync(userId, dto);
        if (!result) return BadRequest("Bu tur zaten favorilerde mevcut.");
        return Ok("Favorilere eklendi.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFavorite(int id, [FromBody] FavoriteTourDto dto)
    {
        if (id != dto.Id) return BadRequest("ID uyuşmuyor.");

        await _favoriteService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        await _favoriteService.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("remove/{tourId}")]
    public async Task<IActionResult> RemoveFavoriteByTourId(int tourId)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var result = await _favoriteService.RemoveFavoriteAsync(userId, tourId);
        if (!result) return NotFound("Favorilerde bulunamadı.");

        return Ok("Favorilerden kaldırıldı.");
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var favorites = await _favoriteService.GetAllAsync();
        return Ok(favorites);
    }
}
