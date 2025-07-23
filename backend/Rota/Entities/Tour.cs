using System;
using System.Xml.Linq;

namespace Entities
{
	public class Tour
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<TourDay> Days { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

