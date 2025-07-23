using System;
namespace Entities
{
	public class TourDay
	{
        public int Id { get; set; }
        public int TourId { get; set; }
        public int DayNumber { get; set; }  // 1, 2, 3...

        public string Description { get; set; }
        public Tour Tour { get; set; }
        public ICollection<TourActivity> Activities { get; set; }
    }
}

