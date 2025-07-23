using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ITourService : IGenericService<TourDto>
	{
		Task<TourDetailsDto> GetTourDetailsAsync(int tourId);
        Task<List<PopularTourDto>> GetPopularToursAsync();
		Task<IEnumerable<TourDto>> SearchAsync(string query);
		Task<List<TourDto>> GetFilteredToursAsync(TourFilterDto filter);
        Task<List<string>> GetCategoriesAsync();

    }
}

