using System;
namespace Entities
{
	public class Payment
	{
        public int Id { get; set; }
        public int ReservationId { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string CardNumberMasked { get; set; }

        public Reservation Reservation { get; set; }
    }
}

