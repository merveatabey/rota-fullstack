using System;
namespace Entities
{
	public class Reservation
	{
        public int Id { get; set; }
        public Guid UserId { get; set; }          // Giriş yapan kullanıcının ID’si
        public int TourId { get; set; }

        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int GuestCount => AdultCount + ChildCount;

        public string Status { get; set; } = "Bekliyor";
        public decimal TotalPrice { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; }

        public string? Note { get; set; }

        public User User { get; set; }
        public Tour Tour { get; set; }
        public Payment Payment { get; set; }
    }
}

