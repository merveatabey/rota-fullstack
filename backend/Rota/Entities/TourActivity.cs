using System;
namespace Entities
{
	public class TourActivity
	{
        public int Id { get; set; }
        public int TourDayId { get; set; }

        public string Time { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ActivityImage { get; set; }

        public TourDay TourDay { get; set; }


    }
}

