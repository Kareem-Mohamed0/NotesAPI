using NotesAPI.Models;

namespace NotesAPI.Repositories
{
    public interface INoteRepository : IGenericRepository<Note>
    {
        Task<IEnumerable<Note>> GetActiveNoteAsync(int userId);
        Task<IEnumerable<Note>> SearchAsync(string query, int userId);
    }
}
