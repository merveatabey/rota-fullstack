using System;
using System.Xml.Linq;

namespace Entities
{
	public class User
	{
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // "Admin", "User", "Guide"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        //Şifre sıfırlama için eklendi
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }


        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FavoriteTour> FavoriteTours { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

        public User()
        {
            Reservations = new HashSet<Reservation>();
            Comments = new HashSet<Comment>();
            FavoriteTours = new HashSet<FavoriteTour>();
            Notifications = new HashSet<Notification>();
            MessagesSent = new HashSet<Message>();
            MessagesReceived = new HashSet<Message>();
        }
    }
}

