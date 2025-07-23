using System;
namespace Entities
{
	public class Hotel
	{
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Star { get; set; }
        public bool IncludedInPrice { get; set; }

        public Tour Tour { get; set; }
    }
}

