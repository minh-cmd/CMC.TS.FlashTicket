namespace CMC.TS.FT.Api.Repositories.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        Task Delete(Guid id);
        Task<T?> GetById(Guid id);
        Task<List<T>?> GetAll();
        Task<int> SaveChangeAsync();
    }
}
