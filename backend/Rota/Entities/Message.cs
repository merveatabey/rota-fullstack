using System;
namespace Entities
{
	public class Message
	{
        public int Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsFromAdmin { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}

