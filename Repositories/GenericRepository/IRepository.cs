namespace CMC.TS.FT.Api.Repositories.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> Create(T? entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(T? entity);
        Task<T?> GetById(Guid id);
        Task<List<T>?> GetAll();
    }
}
