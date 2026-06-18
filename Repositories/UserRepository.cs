using Microsoft.EntityFrameworkCore;
using NotesAPI.Data;
using NotesAPI.Models;

namespace NotesAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<User?> GetByUserNameAsync(string username)
        {
            return await dbset.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
