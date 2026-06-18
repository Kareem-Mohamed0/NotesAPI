using NotesAPI.Data;

namespace NotesAPI.Repositories
{
    public class UnitOfWrok : IUnitOfwork
    {
        private readonly AppDbContext db;
        private INoteRepository? notes;
        private IUserRepository? users;
        public UnitOfWrok(AppDbContext db)
        {
            this.db = db;
        }

        public INoteRepository Notes => notes ??= new NoteRepository(db);
        public IUserRepository Users => users ??= new UserRepository(db);

        public async Task<int> CompleteAsync()
        {
            return await db.SaveChangesAsync();
        }
    }
}
