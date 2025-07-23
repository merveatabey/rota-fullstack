using System;
using Entities;

namespace Rota.Core.Interfaces
{
	public interface IFavoriteRepository
	{
        Task<List<FavoriteTour>> GetFavoritesByUserIdAsync(Guid userId);
        Task<FavoriteTour> GetByUserIdAndTourIdAsync(Guid userId, int tourId);


    }
}

