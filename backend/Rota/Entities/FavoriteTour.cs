using System;
namespace Entities
{
	public class FavoriteTour
	{
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TourId { get; set; }

        public Tour Tour { get; set; }
    }
}
