using System;
namespace Rota.Entities.DTOs
{
	public class FavoriteTourDto
	{
        public int Id { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string TourImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}

