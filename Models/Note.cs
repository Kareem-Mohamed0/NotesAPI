using System.ComponentModel.DataAnnotations.Schema;

namespace NotesAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string Color { get; set; } = "#ffffff";
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public string? DrawingData { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
