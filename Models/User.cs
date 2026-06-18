using Microsoft.AspNetCore.Identity;

namespace NotesAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
