using System;
namespace Rota.Entities.DTOs
{
	public class TourDayDto
	{
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string Description { get; set; }

        public int TourId { get; set; }

        public List<TourActivityDto>? Activities { get; set; } = null;



    }
}

