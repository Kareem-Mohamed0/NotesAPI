using NotesAPI.Models;

namespace NotesAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
