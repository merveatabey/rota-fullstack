using System;
namespace Rota.Entities.DTOs
{
	public class ReservationCreateDto
	{
        public Guid UserId { get; set; }
        public int TourId { get; set; }
        public int GuestCount { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Note { get; set; }
    }
}

