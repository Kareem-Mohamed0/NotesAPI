using Microsoft.EntityFrameworkCore;
using NotesAPI.Data;
using NotesAPI.Models;

namespace NotesAPI.Repositories
{
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {

        public NoteRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Note>> GetActiveNoteAsync(int userId)
        {
            return await dbset
                .Where(n => !n.IsArchived && n.UserId == userId)
                .OrderByDescending(n=> n.IsPinned)
                .ThenByDescending(n=> n.UpdatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> SearchAsync(string query, int userId)
        {
            return await dbset
                .Where(n => n.UserId == userId && !n.IsArchived && (n.Title.Contains(query) || (n.Content != null && n.Content.Contains(query))))
                .OrderByDescending(n => n.IsPinned)
                .ThenBy(n => n.UpdatedAt)
                .ToListAsync();
        }
    }
}
