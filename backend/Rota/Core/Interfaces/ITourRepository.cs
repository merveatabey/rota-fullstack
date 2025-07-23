using System;
using Entities;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ITourRepository : IGenericRepository<Tour>
	{
		Task<List<Tour>> GetPopularToursAsync();
		Task<IEnumerable<Tour>> SearchAsync(string query);
		Task<List<Tour>> GetFilteredTourAsync(TourFilterDto filter);
		Task<List<string>> GetDistinctCategoriesAsync();
	}
}

