 using System;
using Entities;

namespace Rota.Entities.DTOs
{
	public class TourActivityDto
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string? ActivityImage { get; set; }
        public int? TourDayId { get; set; }  //  aktivitenin hangi güne ait olduğunu belirtir. tur ilişkisi burası üzerinden yapılacak


    }
}

