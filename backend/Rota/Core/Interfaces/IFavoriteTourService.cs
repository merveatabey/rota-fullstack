using System;
using Entities;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface IFavoriteTourService : IGenericService<FavoriteTourDto>
	{
        Task<bool> AddFavoriteAsync(Guid userId, FavoriteTourAddDto dto);
        Task<bool> RemoveFavoriteAsync(Guid userId, int tourId);
        Task<IEnumerable<FavoriteTourDto>> GetFavoritesByUserIdAsync(Guid userId);
    }
}

