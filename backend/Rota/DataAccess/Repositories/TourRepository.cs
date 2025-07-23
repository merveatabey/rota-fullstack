using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.DataAccess.Repositories
{
	public class TourRepository : GenericRepository<Tour>, ITourRepository
	{
        private readonly AppDbContext _context;

        public TourRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetDistinctCategoriesAsync()
        {
           return await _context.Tours.Select(t => t.Category)
                .Where(c => !string.IsNullOrEmpty(c)).Distinct().ToListAsync();
        }

        public async Task<List<Tour>> GetFilteredTourAsync(TourFilterDto filter)
        {
            var query = _context.Tours.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Category))
            {
                query = query.Where(t => t.Category == filter.Category);
            }
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(t => t.Price >= filter.MinPrice.Value);
            }
            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(t => t.Price <= filter.MaxPrice.Value);
            }


            query = filter.SortBy switch
            {
                "price-asc" => query.OrderBy(t => t.Price),
                "price-desc" => query.OrderByDescending(t => t.Price),
                "title-asc" => query.OrderBy(t => t.Title),
                "title-desc" => query.OrderByDescending(t => t.Title),
                "date-nearest" => query.OrderBy(t => t.StartDate),
                "date-farthest" => query.OrderByDescending(t => t.StartDate),
                _ => query.OrderBy(t => t.Title)

            };
            return await query.ToListAsync();

        }

        public async Task<List<Tour>> GetPopularToursAsync()
            {
            return await _context.Tours
    .Include(t => t.Comments)
    .Where(t => t.Comments.Any())
    .Where(t => t.Comments.Average(c => c.Rating) >= 4)
    .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> SearchAsync(string query)
        {
            return await _context.Tours
                .Where(t => t.Title.Contains(query)).ToListAsync();

           
        }
    }
}

