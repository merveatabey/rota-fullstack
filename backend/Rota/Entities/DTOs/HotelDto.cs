using System;
namespace Rota.Entities.DTOs
{
	public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Star { get; set; }
        public bool IncludedInPrice { get; set; }
        public int TourId { get; set; }
    }

}

