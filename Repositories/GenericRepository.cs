using Microsoft.EntityFrameworkCore;
using NotesAPI.Data;

namespace NotesAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext db;
        protected readonly DbSet<T> dbset;

        public GenericRepository(AppDbContext db)
        {
            this.db = db;
            dbset = db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void Update(T entity)
        {
            dbset.Update(entity);
        }
    }
}
