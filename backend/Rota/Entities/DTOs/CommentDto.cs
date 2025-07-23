using System;
namespace Rota.Entities.DTOs
{
	public class CommentDto
	{
        public int? Id { get; set; } // Yeni yorumda null olabilir
        public Guid UserId { get; set; }
        public int TourId { get; set; }

        public string CommentText { get; set; }
        public int Rating { get; set; }

        public DateTime? CreatedAt { get; set; } // Yorum eklendikten sonra dönecek

        public string? FullName { get; set; } // Listelemede gösterim için (optional)
    }
}

