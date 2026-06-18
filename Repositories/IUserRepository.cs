using NotesAPI.Models;

namespace NotesAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUserNameAsync(string username);
    }
}
