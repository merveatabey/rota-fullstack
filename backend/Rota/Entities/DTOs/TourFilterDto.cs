using System;
namespace Rota.Entities.DTOs
{
	public class TourFilterDto
	{
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }
    }
}

