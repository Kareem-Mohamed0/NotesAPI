using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;

namespace NotesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
