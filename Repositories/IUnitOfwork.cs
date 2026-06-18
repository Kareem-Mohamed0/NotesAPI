namespace NotesAPI.Repositories
{
    public interface IUnitOfwork
    {
        public INoteRepository Notes { get;}
        public IUserRepository Users { get;}
        Task<int> CompleteAsync();
    }
}
