using System;
namespace Entities
{
	public class Comment
	{
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TourId { get; set; }

        public string CommentText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Tour Tour { get; set; }
    }
}

